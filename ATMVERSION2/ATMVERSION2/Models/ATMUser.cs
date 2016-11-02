using ATMVERSION2.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.Models
{
    public class ATMUser : Model
    {
        protected string cardnumber;
        protected string pin;
        protected string accountNumber;
        protected bool authenticated;
        protected double balance;

        public ATMUser(string cardnumber)
        {
            authenticated = false;
            this.cardnumber = cardnumber;
            this.pin = "";
            this.balance = 0;
            this.accountNumber="";

            setupPin();
            setupAccountNumber();
            retrieveBalance();

            
        }

        public void setupPin()
        {

            SqlConnection myConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Salam\\Source\\Repos\\NetCash\\WebApplication5\\App_Data\\MarkIsGay.mdf; Integrated Security = True; Connect Timeout = 30");
            try
            {
                myConnection.Open();
                Debug.WriteLine("Well done!");
                if (myConnection.State == System.Data.ConnectionState.Open)
                {
                    string query = "SELECT PIN from ATMUsers WHERE CARDNUMBER=" + this.cardnumber;
                    SqlCommand cmd = new SqlCommand(query, myConnection);
                  var reader =  cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        this.pin = reader.GetString(reader.GetOrdinal("PIN"));
                        this.pin = this.pin.Replace(" ", "");
                        Debug.WriteLine("---------------------------PIN RETRIEVED----------------------------"+this.pin);
                       
                    }
                }

            }
            catch (SqlException ex)
            {
                Debug.WriteLine("You failed!" + ex.Message);
            }

        }

        public void setupAccountNumber()
        {

            SqlConnection myConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Salam\\Source\\Repos\\NetCash\\WebApplication5\\App_Data\\MarkIsGay.mdf; Integrated Security = True; Connect Timeout = 30");
            try
            {
                myConnection.Open();
                Debug.WriteLine("Well done!");
                if (myConnection.State == System.Data.ConnectionState.Open)
                {
                    string query = "SELECT AccountNumber from ATMUsers WHERE CARDNUMBER=" + this.cardnumber+ "AND PIN="+this.pin;
                    SqlCommand cmd = new SqlCommand(query, myConnection);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        this.accountNumber = reader.GetString(reader.GetOrdinal("AccountNumber"));
                        this.accountNumber = this.accountNumber.Replace(" ", "");
                        Debug.WriteLine("---------------------------AccountNumber RETRIEVED----------------------------" + this.accountNumber);
                        
                    }
                }

            }
            catch (SqlException ex)
            {
                Debug.WriteLine("You failed!" + ex.Message);
            }

        }

        public double getBalance()
        {
            return this.balance;
        }
        public void setBalance(double amount)
        {
            this.balance+=amount;
            updateBalance();
        }

        public void retrieveBalance()
        {

            SqlConnection myConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Salam\\Source\\Repos\\NetCash\\WebApplication5\\App_Data\\MarkIsGay.mdf; Integrated Security = True; Connect Timeout = 30");
            try
            {
                myConnection.Open();
               
                if (myConnection.State == System.Data.ConnectionState.Open)
                {
                    string query = "SELECT Balance from Account WHERE AccountNumber=" + this.accountNumber;
                    SqlCommand cmd = new SqlCommand(query, myConnection);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        this.balance = double.Parse(reader[0].ToString());

                        Debug.WriteLine("---------------------------Balance RETRIEVED----------------------------" + this.balance);
                    }
                    else { Debug.WriteLine("You failed!"); }
                }

            }
            catch (SqlException ex)
            {
                Debug.WriteLine("You failed!" + ex.Message);
            }

        }

   
        public bool attempToVerify(String pin)
        {
            setupPin();
            //compare cardnumber and passed pin to database version
            if (pin.Length == 4)
            {
                if (this.pin.Equals(pin))
                {
                    Debug.WriteLine("ATM Account is authorized to proceed");
                    //authorized not set true or false
                    return true;
                }
                else
                {
                    Debug.WriteLine("Incorrect pin entered, decrement attempts left");
                    return false;
                }
            }
            else return false;
        }

        public void updateBalance()
        {
            retrieveBalance();
            SqlConnection myConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Salam\\Source\\Repos\\NetCash\\WebApplication5\\App_Data\\MarkIsGay.mdf; Integrated Security = True; Connect Timeout = 30");
            try
            {
                SqlCommand cmd = new SqlCommand();
                myConnection.Open();
                cmd.Connection = myConnection;
                if (myConnection.State == System.Data.ConnectionState.Open)
                {
                    cmd.CommandText = "UPDATE Account SET Balance = @bal WHERE AccountNumber = @acc";

                    cmd.Parameters.AddWithValue("bal", this.balance);
                    cmd.Parameters.AddWithValue("acc", this.accountNumber);
                    cmd.ExecuteNonQuery();
                    Debug.WriteLine("Balance Update Successfull");
                }

            }
            catch (SqlException ex)
            {
                Debug.WriteLine("You failed!" + ex.Message);
            }

        }
        public void updatePin()
        {
            SqlConnection myConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Salam\\Source\\Repos\\NetCash\\WebApplication5\\App_Data\\MarkIsGay.mdf; Integrated Security = True; Connect Timeout = 30");
            try
            {
                SqlCommand cmd = new SqlCommand();
                myConnection.Open();
                cmd.Connection = myConnection;
                if (myConnection.State == System.Data.ConnectionState.Open)
                {
                    cmd.CommandText = "UPDATE ATMUsers SET PIN = @pin WHERE AccountNumber = @acc AND CardNumber = @card";

                    cmd.Parameters.AddWithValue("pin", this.pin);
                    cmd.Parameters.AddWithValue("acc", this.accountNumber);
                    cmd.Parameters.AddWithValue("card", this.cardnumber);
                    cmd.ExecuteNonQuery();
                    Debug.WriteLine("PIN Updated Successfull");
                }

            }
            catch (SqlException ex)
            {
                Debug.WriteLine("You failed!" + ex.Message);
            }

        }

        public void resetPin(string s)
        {
            Debug.WriteLine(s);
            if (s.Length == 4)
            {
                this.pin = s;
                updatePin();

            }
        }
    }
}
