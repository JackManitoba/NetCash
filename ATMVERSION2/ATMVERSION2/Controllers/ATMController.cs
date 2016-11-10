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
        int ChancesLeft = 3;
        bool canceled = false;

        public List<Subject> subjectList
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ATMController(ATMAccount m, ATMMainView v)
        {
            var path = Path.GetFullPath(((AppDomain.CurrentDomain.BaseDirectory).Replace("\\ATMVERSION2\\WindowsFormsApplication1\\bin\\Debug", "")) + "\\WebApplication5\\App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            account = m;
            mainView = v;
            mainView.registerObserver(this);
        }


        public void insertCard(string cardLocation)
        {            
            CardReader CR = new CardReader(cardLocation);
            currentCardNumber = CR.getCardNumber();
            canceled = CR.isCardCanceled(); 
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
                if (canceled == false)
                {                    
                    bool validate = account.IsValid(p.getInput().Text);
                    if (validate)
                    {
                        ChancesLeft = 3;
                        setPanel(pf.getPanel(navClass.getNavigationPanelName()));
                    }
                    else
                    {
                        if (ChancesLeft != 0)
                        {
                            ChancesLeft--;
                            insertCard("");
                            setPanel(pf.getPanel("PIN"));
                            if(ChancesLeft != 0)
                                p.DisplayMessage("INCORRECT PIN, YOU HAVE " + (ChancesLeft + 1) + " ENTRIES LEFT");
                            else
                                p.DisplayMessage("INCORRECT PIN, YOU HAVE " + (ChancesLeft + 1) + " ENTRY LEFT");
                        }
                        else
                        {
                            setPanel(pf.getPanel("PIN"));
                            p.DisplayMessage("INCORRECT PIN, YOUR CARD HAS BEEN CANCELED");
                            CancelCard Canceler = new CancelCard(currentCardNumber);
                            canceled = true;
                        }
                    }
                }
                else
                {
                    setPanel(pf.getPanel("PIN"));
                    p.DisplayMessage("THIS CARD HAS BEEN CANCELED");
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
                    if (p.getInput().Text != "")
                    {
                        double amount = double.Parse(p.getInput().Text);
                        ATMCashManager cashManager = new ATMCashManager();
                        if (account.AreFullFundsAvailable(amount) && cashManager.isWithdrawable(amount))
                        {
                            ATMTransaction withdrawal = new ATMTransaction(account.cardNumber, "WITHDRAWAL", amount);
                            performTransaction(withdrawal);
                            
                            cashManager.UpdateAmountWithdrawal(amount);
                        }
                        else
                        {
                            p.DisplayMessage("INSUFFIECIENT FUNDS FOR THIS TRANSACTION");
                            //set it so that it doesn't navigate away from withdraw panel

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
                        ATMCashManager cashManager = new ATMCashManager();
                        cashManager.UpdateAmountDeposit(amount);
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
