using System.Diagnostics;
using BankingFramework.DatabaseManagement;

namespace BankingFramework.AccountManager
{
    public class ATMAccount : Account
    {
        private string _pin;
        private string _cardNumber;

        public ATMAccount(string accountNumber) : base(accountNumber)
        {
            _pin = GetPin();
        }

        private string GetPin()
        { 
            return DatabaseManager.GetInstance().GetATMAccountPin(GetAccountNumber());
        }

        public bool IsCardCancelled()
        {
            return DatabaseManager.GetInstance().IsCardCancelled(_cardNumber);
        }

        public bool IsValid(string s)
        {
            string currentPin = GetPin();
            if (s == currentPin) return true;
            else return false;
        }

         public void UpdatePin(string newPin)
        {
            DatabaseManager.GetInstance().UpdateATMPinNumber(GetAccountNumber(), newPin);
            _pin = DatabaseManager.GetInstance().GetATMAccountPin(GetAccountNumber());
        }

        public static string GetAccountByCardNumber(string cardNumber)
        {
           return DatabaseManager.GetAccountByATMCardNumber(cardNumber);
        }

        public void SetCardNumber(string cardNumber)
        {
            _cardNumber = cardNumber;
        } 
    }
}
