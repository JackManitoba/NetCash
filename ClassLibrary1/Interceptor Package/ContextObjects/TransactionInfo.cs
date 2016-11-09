﻿using Helpers.AccountManager;
using System;

namespace Helpers.Interceptor_Package
{
   public  class TransactionInfo : ContextObject
    {
        ATMAccount account;
        string description;
        int transactionAmount;
    public TransactionInfo(ATMAccount account, string description, int amount)
        {
            this.account = account;
            this.description = description;
            this.transactionAmount = amount;
        }


      public string  getAccountNumber()
        { return this.account.AccountNumber; }

        public string getAccountBalance()
        { return this.account.Balance.ToString(); }

        public string getAccountCard()
        { return this.account.cardNumber; }

        public string getDescription()
        { return this.description; }
        public string getAmount()
        { return this.transactionAmount.ToString();
        }

        public string getObj()
        {
            return "TransactionInfo: ";
        }

       

        public string getShortDescription()
        { return this.description; }

        public string getVerboseDescription()
        { return "Account: "+ getAccountNumber() + " Description: " + description+ " Amount: € " + getAmount()+ " "  + DateTime.Now; }
    }
}