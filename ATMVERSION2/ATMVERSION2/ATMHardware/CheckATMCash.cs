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
        private int total = 0;

        public CheckATMCash()
        {
            setLocalCash();
        }
        private void setLocalCash()
        {
            string [] denominations = { "1Euro", "2Euro", "5Euro", "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };
            int [] values = { 1, 2, 5, 10, 20, 50, 100, 200, 500 };
            int i = 0;
            while(i < denominations.Length)
            {
                this.total += (setEachDenomination(denominations[i]) * values[i]);
                i++;
            }
            Debug.WriteLine("ATM total = " + total);
        }
        private int setEachDenomination(string current)
        {
            int returnValue;
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var path = baseDir.Replace("\\ATMVERSION2\\WindowsFormsApplication1\\bin\\Debug", "");
            path += "\\WebApplication5\\App_Data";
            var fullPath = Path.GetFullPath(path);
            AppDomain.CurrentDomain.SetData("DataDirectory", fullPath);

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [Amount] From [dbo].[ATMCash] WHERE [Note] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = current;

                connection.Open();
                returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                Debug.WriteLine(current + " : " + returnValue);
                cmd.Dispose();
                connection.Dispose();
            }
            return returnValue;
        }
        public Boolean isWithdrawable(int attemptedWithdrawal)
        {
            Boolean isValid = true;
            if (attemptedWithdrawal >= this.total)
                isValid = false;
            return isValid;
        }
    }
}
