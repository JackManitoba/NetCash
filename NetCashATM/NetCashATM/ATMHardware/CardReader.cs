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
using BankingFramework.Utils;

namespace NetCashATM.ATMHardware
{    
    public class CardReader
    {
        private Card _currentCard;
        
        public CardReader(string cardLocation)
        {
            if (cardLocation != "")
            {
                ReadCardFromFile(cardLocation);
            }
            else
            {
                var path = Directory.GetCurrentDirectory();
                path += "\\NetCash_Debit_Card.txt";
                ReadCardFromFile(path);
            }
        }
        private void ReadCardFromFile(string cardLocation)
        {           
            string[] lines = File.ReadAllLines(@cardLocation);
            string CNO = lines[0].Replace("Card Number: ", "");
            string E = lines[1].Replace("Expiry Date: ", "");           
            bool Canceled = false;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [Cancelled] From [dbo].[Users] WHERE [CardNumber] = @a ";
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = CNO;

                connection.Open();
                Canceled = Convert.ToBoolean(cmd.ExecuteScalar());
                cmd.Dispose();
                connection.Dispose();
            }

            Debug.WriteLine(lines[0]);
            Debug.WriteLine(lines[1]);
            _currentCard = new Card(CNO, E, Canceled);
        }

        public string GetCardNumber()
        {
            return _currentCard.GetCardNumber();
        }

        public bool IsCardCanceled()
        {
            return _currentCard.IsCardCanceled();
        }        
    }
}
