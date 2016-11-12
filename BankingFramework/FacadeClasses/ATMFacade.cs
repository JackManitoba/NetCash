using BankingFramework.AccountManager;
using BankingFramework.BankTransactions;
using BankingFramework.CardManager;
using BankingFramework.Interceptor_Package;
using BankingFramework.Interceptor_Package.Dispatchers;

namespace BankingFramework.FacadeClasses
{
    public class ATMFacade
    {
        private ATMAccount ATMAccount;

        public ATMFacade(string cardnumber)
        {
            ATMAccount = new ATMAccount(ATMAccount.GetAccountByCardNumber(cardnumber));
            ATMAccount.CardNumber = cardnumber;
        }

        public void UpdateAccountPinNumber(string newPin)
        {
            ATMAccount.UpdatePin(newPin);
        }

        public string GetAccountBalance()
        {
            return ATMAccount.Balance.ToString();
        }

        public bool ValidateAccount(string text)
        {
            return ATMAccount.IsValid(text);

        }

        public void CancelCard(string currentCardNumber)
        {
            CardManager.CardManager.CancelCard(currentCardNumber);
        }

        public void PerformWithdraw(double amount)
        {
            Transaction withdrawal = new Withdrawal(ATMAccount, "WITHDRAWAL", amount);

            if (withdrawal.AreFundsAvailable())
            {
                withdrawal.PerformTransaction();
                ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(ATMAccount, withdrawal.GetType(), withdrawal.GetAmount()));
            }
        }

        public void PerformDeposit(double amount)
        {
            Transaction deposit = new Deposit(ATMAccount, "DEPOSIT", amount);        
            deposit.PerformTransaction();

            ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(this.ATMAccount, deposit.GetType(), deposit.GetAmount()));
        }

        public bool AreFundsAvailable(double amount)
        {
            return ATMAccount.AreFundsAvailable(amount);         
        }

        public string ReturnAccountBalance()
        {
            ATMAccount.UpdateAccountBalance();
            return ATMAccount.Balance.ToString();
        }

        public string GetAccountNumber()
        {
            return ATMAccount.AccountNumber;
        }

        public static string GetAccountByCardNumber(string currentCardNumber)
        {
            return ATMAccount.GetAccountByCardNumber(currentCardNumber);
        }
    }
}
