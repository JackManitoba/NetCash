﻿using BankingFramework.AccountManager;
using BankingFramework.BankTransactions;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;

namespace BankingFramework.FacadeClasses
{
    public class ATMFacade
    {
        private ATMAccount _atmAccount;

        public ATMFacade(string cardnumber)
        {
            _atmAccount = new ATMAccount(ATMAccount.GetAccountByCardNumber(cardnumber));
            _atmAccount.CardNumber = cardnumber;
        }

        public void UpdateAccountPinNumber(string newPin)
        {
            _atmAccount.UpdatePin(newPin);
        }

        public string GetAccountBalance()
        {
            return _atmAccount.Balance.ToString();
        }

        public bool ValidateAccount(string text)
        {
            return _atmAccount.IsValid(text);

        }

        public void CancelCard(string currentCardNumber)
        {
            CardManager.CardManager.CancelCard(currentCardNumber);
        }

        public void PerformWithdraw(double amount)
        {
            Transaction withdrawal = new Withdrawal(_atmAccount, "WITHDRAWAL", amount);

            if (withdrawal.AreFundsAvailable())
            {
                withdrawal.PerformTransaction();
                
            }
        }

        public void PerformDeposit(double amount)
        {
            Transaction deposit = new Deposit(_atmAccount, "DEPOSIT", amount);        
            deposit.PerformTransaction();

           }

        public bool AreFundsAvailable(double amount)
        {
            return _atmAccount.AreFundsAvailable(amount);         
        }

        public string ReturnAccountBalance()
        {
            _atmAccount.UpdateAccountBalance();
            return _atmAccount.Balance.ToString();
        }

        public string GetAccountNumber()
        {
            return _atmAccount.AccountNumber;
        }

        public static string GetAccountByCardNumber(string currentCardNumber)
        {
            return ATMAccount.GetAccountByCardNumber(currentCardNumber);
        }

        public bool IsCardCancelled()
        {
            return _atmAccount.IsCardCancelled();
        }
    }
}
