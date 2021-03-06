﻿using BankingFramework.Logging;
using System;

namespace BankingFramework.InterceptorPackage.ContextObjects
{
   public  class TransactionInfoContextObject : ContextObject
    {
        private string _account;
        private string _description;
        private double _transactionAmount;

        public TransactionInfoContextObject(string accountNumber, string description, double amount)
        {
            _account = accountNumber;
            _description = description;
            _transactionAmount = amount;
        }

        public string GetAccountNumber()
        {
            return _account;
        }

        public string GetAmount()
        {
            return _transactionAmount.ToString();
        }

        public string GetObj()
        {
            return "TransactionInfo: ";
        }

        public string GetShortDescription()
        {
            return _description;
        }

        public string GetVerboseDescription()
        {
            return "Account: " + GetAccountNumber() + " Description: " + _description + " Amount: € " + GetAmount() + " " + DateTime.Now;
        }

        public void Service()
        {
            Logger l = new Logger();
            l.LogAccountTransactions(GetAccountNumber(),GetVerboseDescription());
        }
    }
}
