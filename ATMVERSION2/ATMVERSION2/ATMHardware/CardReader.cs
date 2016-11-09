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
    class CardReader
    {
        private Card currentCard;

        public CardReader(string cardLocation)
        {
            if (cardLocation != "")
                readCardFromFile(cardLocation);
            else
            {
                var path = Directory.GetCurrentDirectory();
                path += "\\NetCash_Debit_Card.txt";
                Debug.WriteLine("+++" + path);
                readCardFromFile(path); 
            }
        }
        private void readCardFromFile(string cardLocation)
        {           
            string[] lines = System.IO.File.ReadAllLines(@cardLocation);
            string CNO = lines[0].Replace("Card Number: ", "");
            string E = lines[1].Replace("Expiry Date: ", "");           
            bool Canceled = false;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [Canceled] From [dbo].[ATMUsers] WHERE [CardNumber] = @a ";

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
            Debug.WriteLine(Canceled);
            currentCard = new Card(CNO, E, Canceled);
        }
        public string getCardNumber()
        {
            return currentCard.getCardNumber();
        }
        public bool isCardCanceled()
        {
            return currentCard.isCardCanceled();
        }        
    }
}
