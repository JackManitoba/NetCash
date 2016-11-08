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
    class UpdateCashATM
    {
        private int[] DenominationsAmounts = new int[9];

        public UpdateCashATM()
        {
            setLocalCash();
        }

        private void setLocalCash()
        {
            string[] denominations = { "1Euro", "2Euro", "5Euro", "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };
            int i = 0;
            while (i < denominations.Length)
            {
                int attempted = setEachDenomination(denominations[i]);
                DenominationsAmounts[i] = attempted;
                i++;
            }
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

        public void UpdateAmountWithdrawal(double updateAmount)
        {
            updatecashWithdrawal(updateAmount);
            int i = 0;
            string[] denominations = { "1Euro", "2Euro", "5Euro", "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };
            while (i < DenominationsAmounts.Length)
            {
                StoreonDatabase(denominations[i], i);
                i++;
            }
        }
        public void UpdateAmountDeposit(double updateAmount)
        {
            updatecashDeposit(updateAmount);
            int i = 0;
            string[] denominations = { "1Euro", "2Euro", "5Euro", "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };
            while (i < DenominationsAmounts.Length)
            {
                StoreonDatabase(denominations[i], i);
                i++;
            }
        }
        private void StoreonDatabase(string note, int amount)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[ATMCash] Set [Amount]=@b WHERE [Note] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = note;
                cmd.Parameters
                   .Add(new SqlParameter("@b", SqlDbType.Int))
                   .Value = DenominationsAmounts[amount];
                connection.Open();
                Debug.WriteLine("+++note=" + note + "  amount = " + DenominationsAmounts[amount]);

                cmd.ExecuteScalar();
                cmd.Dispose();
                connection.Dispose();
            }
        }

        private void updatecashWithdrawal(double doubleattempt)
        {
            int attempted = Convert.ToInt32(doubleattempt);
            int[] Denominations = { 1, 2, 5, 10, 20, 50, 100, 200, 500 };
            Debug.WriteLine("+++Current amount = " + attempted);
            for (int i = Denominations.Length - 1; i >= 0; i--)
            {
                Debug.WriteLine("+++Current amount is now, " + attempted);
                if (Denominations[i] > attempted)
                {
                    //do nothing
                    Debug.WriteLine("+++Current Denomination, " + Denominations[i] + " is GREATER than current amount");
                }
                else if (Denominations[i] == attempted)
                {
                    if (DenominationsAmounts[i] != 0)
                    {
                        attempted -= Denominations[i];
                        DenominationsAmounts[i]--;
                        Debug.WriteLine("+++Current Denomination, " + Denominations[i] + " is EQUAL than current amount and there is at least one of those notes");
                        i = -1;
                    }
                    else
                        Debug.WriteLine("+++Current Denomination, " + Denominations[i] + " is EQUAL than current amount NO NOTES OF THIS TYPE");
                }
                else if (Denominations[i] < attempted)
                {
                    if (DenominationsAmounts[i] != 0)
                    {
                        Debug.WriteLine("+++Current Denomination, " + Denominations[i] + " is LESS than current amount and there is at least one of those notes");
                        attempted -= Denominations[i];
                        DenominationsAmounts[i]--;
                        if (attempted != 0)
                            i++;
                    }
                }
            }
        }
        private void updatecashDeposit(double doubleattempt)
        {
            int attempted = Convert.ToInt32(doubleattempt);
            int[] Denominations = { 1, 2, 5, 10, 20, 50, 100, 200, 500 };
            Debug.WriteLine("+++Current amount = " + attempted);
            for (int i = Denominations.Length - 1; i >= 0; i--)
            {
                Debug.WriteLine("+++Current amount is now, " + attempted);
                if (Denominations[i] > attempted)
                {
                    //do nothing
                    Debug.WriteLine("+++Current Denomination, " + Denominations[i] + " is GREATER than current amount");
                }
                else if (Denominations[i] == attempted)
                {
                    attempted -= Denominations[i];
                    DenominationsAmounts[i]++;
                    Debug.WriteLine("+++Current Denomination, " + Denominations[i] + " is EQUAL than current amount and there is at least one of those notes");
                    i = -1;
                }
                else if (Denominations[i] < attempted)
                {
                    Debug.WriteLine("+++Current Denomination, " + Denominations[i] + " is LESS than current amount and there is at least one of those notes");
                    attempted -= Denominations[i];
                    DenominationsAmounts[i]++;
                    if (attempted != 0)
                        i++;
                }
            }
        }
    }
}
