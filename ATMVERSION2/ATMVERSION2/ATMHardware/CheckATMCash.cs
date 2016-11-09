using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.ATMHardware
{
    class CheckATMCash
    {
        private double total = 0;
        private int[] DenominationsAmounts = new int[6];

        public CheckATMCash()
        {
            setLocalCash();
        }
        private void setLocalCash()
        {
            string [] denominations = { "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };
            double [] values = { 10, 20, 50, 100, 200, 500 };
            int i = 0;
            while(i < denominations.Length)
            {
                int attempted = setEachDenomination(denominations[i]);
                DenominationsAmounts[i] = attempted;
                this.total +=  attempted * values[i];
                i++;
            }
            Debug.WriteLine("ATM total = " + total);
        }
        private int setEachDenomination(string current)
        {
            int returnValue;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [Amount] From [dbo].[ATMCash] WHERE [Note] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = current;

                connection.Open();
                returnValue = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                Debug.WriteLine(current + " : " + returnValue);
                cmd.Dispose();
                connection.Dispose();
            }
            return returnValue;
        }

        public bool isWithdrawable(double attemptedWithdrawal)
        {
            bool ReturnVal = CheckBank(attemptedWithdrawal);
            return ReturnVal;
        }

        private bool CheckBank(double doubleattempt)
        {
            int attempted = Convert.ToInt32(doubleattempt);
            int[] Denominations = { 10, 20, 50, 100, 200, 500};
            Debug.WriteLine("Current amount = " + attempted);
            if (attempted <= this.total)
            {
                for (int i = Denominations.Length -1; i >= 0; i--)
                {
                    Debug.WriteLine("Current amount is now, " + attempted );
                    if (Denominations[i] > attempted)
                    {
                        //do nothing
                        Debug.WriteLine("Current Denomination, " + Denominations[i] + " is GREATER than current amount");
                    }
                    else if (Denominations[i] == attempted)
                    {
                        if(DenominationsAmounts[i] != 0)
                        {
                            attempted -= Denominations[i];
                            Debug.WriteLine("Current Denomination, " + Denominations[i] + " is EQUAL than current amount and there is at least one of those notes");
                            i = -1;
                        }
                        else
                            Debug.WriteLine("Current Denomination, " + Denominations[i] + " is EQUAL than current amount NO NOTES OF THIS TYPE");
                    }
                    else if (Denominations[i] < attempted)
                    {
                        if (DenominationsAmounts[i] != 0)
                        {
                            Debug.WriteLine("Current Denomination, " + Denominations[i] + " is LESS than current amount and there is at least one of those notes");
                            attempted -= Denominations[i];
                            DenominationsAmounts[i]--;
                            if (attempted != 0)
                                i++;
                        }
                    }
                }
            }
            if (attempted == 0)
                return true;
            else
                return false;
        }
    }
}
