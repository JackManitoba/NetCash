using BankingFramework.Utils;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System;

namespace BankingFramework.AccountManager
{
    public class ATMAccount : Account
    {
        [Required]
        [Display(Name = "PIN")]
        public string Pin { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        public ATMAccount(string accountNumber) : base(accountNumber)
        {
            Pin = GetPin();
            Debug.WriteLine("Account pin: " + Pin);
        }

        private string GetPin()
        { 
            return DatabaseManager.GetInstance().GetATMAccountPin(this.AccountNumber);
        }

        public bool IsCardCancelled()
        {
            return DatabaseManager.GetInstance().IsCardCancelled(this.CardNumber);
        }

        public bool IsValid(string s)
        {
            string currentPin = GetPin();
            Debug.WriteLine(s + " ----- " + currentPin);
            if (s == currentPin) return true;
            else return false;
        }

         public void UpdatePin(string newPin)
        {
            DatabaseManager.GetInstance().UpdateATMPinNumber(this.AccountNumber, newPin);
            Pin = DatabaseManager.GetInstance().GetATMAccountPin(AccountNumber);
    
        }

        public static string GetAccountByCardNumber(string _cardNumber)
        {
           return DatabaseManager.GetAccountByATMCardNumber(_cardNumber);
        }
    }
}
