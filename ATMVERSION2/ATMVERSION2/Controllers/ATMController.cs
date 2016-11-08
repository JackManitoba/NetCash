using ATMVERSION2.Interfaces;
using System;
using System.Collections.Generic;
using ATMVERSION2.UserInterface.Panels;
using ATMVERSION2.Views;
using WebApplication5.Models.ATMModels;

using ATMVERSION2.HelperClasses;
using System.Windows.Forms;

using System.Diagnostics;
using ATMVERSION2.Views.UserInterface.Panels;
using ATMVERSION2.ATMHardware;
using Helpers.AccountManager;
using Helpers.Interceptor_Package.Dispatchers;
using Helpers.Interceptor_Package;
using System.IO;

namespace ATMVERSION2.Controllers
{
    public class ATMController : Observer
    {
        ATMAccount account;
        static ATMMainView mainView;
        string currentCardNumber = "";

        public List<Subject> subjectList
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ATMController(ATMAccount m, ATMMainView v)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var path = baseDir.Replace("\\ATMVERSION2\\WindowsFormsApplication1\\bin\\Debug", "");
            path += "\\WebApplication5\\App_Data";
            var fullPath = Path.GetFullPath(path);
            AppDomain.CurrentDomain.SetData("DataDirectory", fullPath);

            account = m;
            mainView = v;
            mainView.registerObserver(this);
        }


        public void insertCard(string cardLocation)
        {
            CardReader CR = new CardReader(cardLocation);
            currentCardNumber = CR.getCardNumber();
            account = new ATMAccount(ATMAccount.getAccountByCardNumber(currentCardNumber));
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
                    insertCard("");
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
                    //if cancel, log out
                    if (p.getInput().Text != "")
                    {
                        double amount = double.Parse(p.getInput().Text);
                        CheckATMCash CashCheck = new CheckATMCash();
                        if (account.AreFullFundsAvailable(amount) && CashCheck.isWithdrawable(amount))
                        {
                            ATMTransaction withdrawal = new ATMTransaction(account.cardNumber, "WITHDRAWAL", amount);
                            performTransaction(withdrawal);
                            UpdateCashATM update = new UpdateCashATM();
                            update.UpdateAmountWithdrawal(amount);
                        }
                        else
                        {
                            Debug.WriteLine("+++Not enough funds");
                            //send to a new panel if you can't withdraw that much
                        }
                    }
                }

                if (mainView.getCurrentPanel().name.Equals("DepositPanel"))
                {
                    DepositPanel p = (DepositPanel)mainView.getCurrentPanel();
                    if (p.getInput().Text != "")
                    {
                        double amount = double.Parse(p.getInput().Text);
                        ATMTransaction deposit = new ATMTransaction(account.cardNumber, "DEPOSIT", amount);
                        performTransaction(deposit);
                        UpdateCashATM update = new UpdateCashATM();
                        update.UpdateAmountDeposit(amount);
                    }
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
