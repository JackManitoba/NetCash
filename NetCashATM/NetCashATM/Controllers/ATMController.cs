using NetCashATM.Interfaces;
using System;
using System.Collections.Generic;
using NetCashATM.UserInterface.Panels;
using NetCashATM.Views;
using NetCashATM.HelperClasses;
using System.Windows.Forms;
using System.Diagnostics;
using NetCashATM.Views.UserInterface.Panels;
using NetCashATM.ATMHardware;
using BankingFramework.AccountManager;
using System.IO;
using BankingFramework.FacadeClasses;

namespace NetCashATM.Controllers
{
    public class ATMController : Observer
    {
        public ATMFacade Facade;
      
        public static ATMMainView MainView;
        public string CurrentCardNumber = "";
        public int ChancesLeft = 3;
        public bool Cancelled = false;
       
        //WHY???
        public List<Subject> SubjectList
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ATMController(ATMAccount m, ATMMainView v)
        {
            var path = Path.GetFullPath(((AppDomain.CurrentDomain.BaseDirectory).Replace("\\NetCashATM\\ClientCode\\bin\\Debug", "")) + "\\NetCashWebSite\\App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            MainView = v;
            MainView.RegisterObserver(this);
        }


        public void InsertCard(string cardLocation)
        {            
            CardReader CR = new CardReader(cardLocation);
            CurrentCardNumber = CR.GetCardNumber();
            Cancelled = CR.IsCardCanceled(); 
            Facade = new ATMFacade(CurrentCardNumber);
        }

        public void ResetAccountPin(string newPin)
        {
            if (newPin.Length == 4)
                Facade.UpdateAccountPinNumber(newPin);
        }

        internal void SetPanel(ATMPanel currentPanel)
        {            
           MainView.SetCurrentPanel(currentPanel);
        }

        public void HandleChange(Subject e)
        {
            NavigationDataClass navClass = MainView.getNavigationClass();
            Debug.WriteLine("CurrentPanel: " + MainView.GetCurrentPanel().Name);
            Debug.WriteLine("NavigationClass selection: " + MainView.getNavigationClass().GetNavigationPanelName());
            PanelFactory pf = new PanelFactory();
            Debug.WriteLine("Why Jack," + MainView.GetCurrentPanel().Name);
            if (MainView.GetCurrentPanel().Name.Equals("PinPanel"))
            {
                PinPanel p = (PinPanel)MainView.GetCurrentPanel();
                if (p.GetInput().Text != "")
                {
                    if (Cancelled == false)
                    {
                        bool validate = Facade.ValidateAccount(p.GetInput().Text);
                        if (validate)
                        {
                            ChancesLeft = 3;
                            SetPanel(pf.GetPanel(navClass.GetNavigationPanelName()));
                        }
                        else
                        {
                            if (ChancesLeft != 0)
                            {
                                ChancesLeft--;
                                InsertCard("");
                                SetPanel(pf.GetPanel("PIN"));
                                if (ChancesLeft != 0)
                                    p.DisplayMessage("INCORRECT PIN, YOU HAVE " + (ChancesLeft + 1) + " ENTRIES LEFT");
                                else
                                    p.DisplayMessage("INCORRECT PIN, YOU HAVE " + (ChancesLeft + 1) + " ENTRY LEFT");
                            }
                            else
                            {
                                SetPanel(pf.GetPanel("PIN"));
                                p.DisplayMessage("INCORRECT PIN, YOUR CARD HAS BEEN CANCELED");
                                Facade.CancelCard(CurrentCardNumber);
                                Cancelled = true;
                            }
                        }
                    }
                    else
                    {
                        SetPanel(pf.GetPanel("PIN"));
                        p.DisplayMessage("THIS CARD HAS BEEN CANCELED");
                    }
                }
            }
            else
            {


                MainView = (ATMMainView)e;
              /*  if (navClass.getNavigationPanelName().Equals("LogoutPanel"))
                { verifiedSession = 0; }*/



                if (MainView.GetCurrentPanel().Name.Equals("PinResetPanel"))
                {
                    PinResetPanel p = (PinResetPanel)MainView.GetCurrentPanel();
                    string newPin = p.GetInput().Text;
                    ResetAccountPin(newPin);
                    //controller.resetAccountPin(newPin);
                }


                if (MainView.GetCurrentPanel().Name.Equals("WithdrawalPanel"))
                {
                    WithdrawalPanel p = (WithdrawalPanel)MainView.GetCurrentPanel();
                    if (p.GetInput().Text != "")
                    {
                        double amount = double.Parse(p.GetInput().Text);
                        ATMCashManager cashManager = new ATMCashManager();
                        
                       
                        if (cashManager.IsWithdrawable(amount)&&Facade.AreFundsAvailable(amount))
                        {
                          

                            Facade.PerformWithdraw(amount);                     
                            cashManager.UpdateAmountWithdrawal(amount);
                        }
                        else
                        {
                            p.DisplayMessage("INSUFFIECIENT FUNDS FOR THIS TRANSACTION");
                            //set it so that it doesn't navigate away from withdraw panel

                        }
                    }
                }


                if (MainView.GetCurrentPanel().Name.Equals("DepositPanel"))
                {
                    DepositPanel p = (DepositPanel)MainView.GetCurrentPanel();
                    if (p.GetInput().Text != "")
                    {
                        double amount = double.Parse(p.GetInput().Text);
                        Facade.PerformDeposit(amount);
                        ATMCashManager cashManager = new ATMCashManager();
                        cashManager.UpdateAmountDeposit(amount);
                    }
                }


                SetPanel(pf.GetPanel(navClass.GetNavigationPanelName()));


                if (MainView.GetCurrentPanel().Name.Equals("BalancePanel"))
                {
                    BalancePanel p = (BalancePanel)MainView.GetCurrentPanel();
                    p.ShowBalance(Facade.ReturnAccountBalance());
                
                }

                if (MainView.GetCurrentPanel().Name.Equals("PrintInfo"))
                {
                    PrintInfo p = (PrintInfo)MainView.GetCurrentPanel();
                    p.SetFileName(Facade.GetAccountNumber());
                }
            }
        }

        public void StartATM()
        {
            SetPanel(new PinPanel());
            Application.Run(MainView);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

       public void Update(Subject e)
        {
            HandleChange(e);
        }
    }
}
