using ATMVERSION2.Interfaces;
using System;
using System.Collections.Generic;
using ATMVERSION2.UserInterface.Panels;
using ATMVERSION2.Views;
using WebApplication5.Models.ATMModels;
using ATMVERSION2.AccountManager;
using ATMVERSION2.HelperClasses;
using System.Windows.Forms;
using WindowsFormsApplication1.Interceptor_Package.Dispatchers;
using WindowsFormsApplication1.Interceptor_Package;
using System.Diagnostics;
using ATMVERSION2.Views.UserInterface.Panels;
using ATMVERSION2.ATMHardware;

namespace ATMVERSION2.Controllers
{
    public class ATMController : Observer
    {
       Account account;
        static ATMMainView mainView;
        string currentCardNumber = "";

        public List<Subject> subjectList
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ATMController(Account m, ATMMainView v)
        {
            account = m;
            mainView = v;
            mainView.registerObserver(this);
        }


        public void insertCard(string cardNumber)
        {
            CardReader CR = new CardReader("");
            currentCardNumber = CR.getCardNumber();
            account = new Account(Account.getAccountByCardNumber(cardNumber));
        }

        public void performTransaction(ATMTransaction transaction)
        {
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(this.account,transaction.type, transaction.amount));
            if (transaction.type.Equals("WITHDRAWAL"))
                account.UpdateAmount(-transaction.amount);
            else
                account.UpdateAmount(transaction.amount);

        }

         public void resetAccountPin(string newPin)
        {
            if(newPin.Length==4)
            account.updatePin(newPin);
        }

        internal void setPanel(ATMPanel currentPanel)
        {
            
           mainView.setCurrentPanel(currentPanel);
        }

        

        public void handleChange(Subject e)
        {
            NavigationDataClass navClass = mainView.getNavigationClass();
            Debug.WriteLine("CurrentPanel: " + mainView.getCurrentPanel().name);
            Debug.WriteLine("NavigationClass selection: " + mainView.getNavigationClass().getNavigationPanelName());
            PanelFactory pf = new PanelFactory();

            if (mainView.getCurrentPanel().name.Equals("PinPanel"))
            {
                PinPanel p = (PinPanel)mainView.getCurrentPanel();
                bool validate = account.IsValid(p.getInput().Text);
                if (validate)
                {
                    setPanel(pf.getPanel(navClass.getNavigationPanelName()));
                }
                else
                {
                    insertCard(currentCardNumber);
                  setPanel(pf.getPanel("PIN"));
                }
            }
            else
            {


                mainView = (ATMMainView)e;
              /*  if (navClass.getNavigationPanelName().Equals("LogoutPanel"))
                { verifiedSession = 0; }*/



                if (mainView.getCurrentPanel().name.Equals("PinResetPanel"))
                {
                    PinResetPanel p = (PinResetPanel)mainView.getCurrentPanel();
                    string newPin = p.getInput().Text;
                    resetAccountPin(newPin);
                    //controller.resetAccountPin(newPin);
                }


                if (mainView.getCurrentPanel().name.Equals("WithdrawalPanel"))
                {
                    WithdrawalPanel p = (WithdrawalPanel)mainView.getCurrentPanel();
                    double amount = double.Parse(p.getInput().Text);
                    ATMTransaction withdrawal = new ATMTransaction(account.cardNumber, "WITHDRAWAL", amount);
                    performTransaction(withdrawal);
                }

                if (mainView.getCurrentPanel().name.Equals("DepositPanel"))
                {
                    DepositPanel p = (DepositPanel)mainView.getCurrentPanel();
                    double amount = double.Parse(p.getInput().Text);
                    ATMTransaction deposit = new ATMTransaction(account.cardNumber, "DEPOSIT", amount);
                   performTransaction(deposit);
                }


               setPanel(pf.getPanel(navClass.getNavigationPanelName()));


                if (mainView.getCurrentPanel().name.Equals("BalancePanel"))
                {
                    BalancePanel p = (BalancePanel)mainView.getCurrentPanel();
                    p.showBalance(account.Balance.ToString());
                }

                if (mainView.getCurrentPanel().name.Equals("PrintInfo"))
                {
                    PrintInfo p = (PrintInfo)mainView.getCurrentPanel();
                    p.setFileName(account.AccountNumber);
                }
            }
        }

        public void startATM()
        {

            setPanel(new PinPanel());
            Application.Run(mainView);
            }

        public void update()
        {
            throw new NotImplementedException();
        }

        public void update(Subject e)
        {
            handleChange(e);
        }
    }
}
