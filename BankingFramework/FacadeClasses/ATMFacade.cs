using BankingFramework.AccountManager;
using BankingFramework.BankTransactions;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using BankingFramework.Statements;
using System.Collections.Generic;

namespace BankingFramework.FacadeClasses
{
    public class ATMFacade
    {
        private ATMAccount _atmAccount;

        public ATMFacade(string cardNumber)
        {
            _atmAccount = new ATMAccount(ATMAccount.GetAccountByCardNumber(cardNumber));
            _atmAccount.SetCardNumber(cardNumber);
        }

        public void UpdateAccountPinNumber(string newPin)
        {
            _atmAccount.UpdatePin(newPin);
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
            return _atmAccount.GetBalance().ToString();
        }

        public string GetAccountNumber()
        {
            return _atmAccount.GetAccountNumber();
        }

        public static string GetAccountByCardNumber(string currentCardNumber)
        {
            return ATMAccount.GetAccountByCardNumber(currentCardNumber);
        }

        public bool IsCardCancelled()
        {
            return _atmAccount.IsCardCancelled();
        }

        public List<List<string>> GetStatement()
        {
            Statement statement = new Statement(_atmAccount.GetAccountNumber());
            return statement.getListOfTransactions();
        }
    }
}
