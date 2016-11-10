using Helpers.AccountManager;
using Helpers.ATMHardware;
using Helpers.BankTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.FacadeClasses
{
    public class ATMFacade
    {
        private ATMAccount account;

        public ATMFacade(string accountNumber)
        {
            account = new ATMAccount(accountNumber);
        }

        public void updateAccountPinNumber(string newPin)
        {
            account.updatePin(newPin);
        }

        public string getAccountBalance()
        {
            return account.Balance.ToString();
        }

        public bool validateAccount(string text)
        {
            return account.IsValid(text);

        }

        public void cancelCard(string currentCardNumber)
        {
            CancelCard Canceler = new CancelCard(currentCardNumber);
        }

        public void performWithdraw(double amount)
        {
            Transaction withdrawal = new Withdrawal(account.cardNumber, "WITHDRAWAL", amount);
            if (withdrawal.AreFundsAvailable())
            {
                withdrawal.PerformTransaction();
            }
        }
        public void performDeposit(double amount)
        {
            Transaction deposit = new Deposit(account.cardNumber, "DEPOSIT", amount);
           
               deposit.PerformTransaction();
            
        }

        public bool areFundsAvailable(double amount)
        {
            return account.AreFundsAvailable(amount);
               
        }

        public string returnAccountBalance()
        {
            return account.Balance.ToString();
        }

        public string getAccountNumber()
        {
            return account.AccountNumber;
        }

        public string getAccountByCardNumber(string currentCardNumber)
        {
            return ATMAccount.getAccountByCardNumber(currentCardNumber);
        }
    }
}
