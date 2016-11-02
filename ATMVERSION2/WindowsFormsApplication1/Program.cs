using ATMVERSION2.Controllers;
using ATMVERSION2.HelperClasses;
using ATMVERSION2.Interfaces;
using ATMVERSION2.Models;
using ATMVERSION2.UserInterface.Panels;
using ATMVERSION2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    class Program : Observer
    {
        public int verifiedSession = 0;
        public List<Subject> subjectList
        {
            get
            {
                return subjectList;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static ATMMainView mainView;
        static Controller controller;
        static ATMUser account;
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //MainView
            mainView = new ATMMainView();

            //model
            insertCard();

            //CurrentView
            ATMPanel currentPanel = new PinPanel();
            //Controller
            controller = new PinVerificationController(account, mainView);
            controller.setPanel(currentPanel);
            // mainView.Activate();
            mainView.registerObserver(new Program());
            Application.Run(mainView);

        }

        public void update()
        {
        }

        public void update(Subject e)
        {
            NavigationDataClass navClass = mainView.getNavigationClass();
            PanelFactory pf = new PanelFactory();


            if (mainView.getCurrentPanel().name.Equals("PinPanel"))
            {
                PinPanel p = (PinPanel)mainView.getCurrentPanel();
                bool validate = account.attempToVerify(p.getInput().Text);
                if (validate)
                {


                    controller.setPanel(pf.getPanel(navClass.getNavigationPanelName()));
                }
                else
                {
                    insertCard();
                    controller.setPanel(pf.getPanel("PIN"));
                }
            }
            else
            {


                mainView = (ATMMainView)e;
                if (navClass.getNavigationPanelName().Equals("LogoutPanel"))
                { verifiedSession = 0; }



                if (mainView.getCurrentPanel().name.Equals("PinResetPanel"))
                {
                    PinResetPanel p = (PinResetPanel)mainView.getCurrentPanel();
                    string newPin = p.getInput().Text;
                    account.resetPin(newPin);
                    //controller.resetAccountPin(newPin);
                }


                if (mainView.getCurrentPanel().name.Equals("WithdrawalPanel"))
                {
                    WithdrawalPanel p = (WithdrawalPanel)mainView.getCurrentPanel();
                    double amount = double.Parse(p.getInput().Text);
                    Withdrawal withdrawal = new Withdrawal(account, amount);
                    controller.performWithdrawal(withdrawal);
                }

                if (mainView.getCurrentPanel().name.Equals("DepositPanel"))
                {
                    DepositPanel p = (DepositPanel)mainView.getCurrentPanel();
                    double amount = double.Parse(p.getInput().Text);
                    Deposit deposit = new Deposit(account, amount);
                    controller.performDeposit(deposit);
                }


                controller.setPanel(pf.getPanel(navClass.getNavigationPanelName()));


                if (mainView.getCurrentPanel().name.Equals("BalancePanel"))
                {
                    BalancePanel p = (BalancePanel)mainView.getCurrentPanel();
                    p.showBalance(account.getBalance().ToString());
                }
            }
        }
        
        public static void insertCard()
        {
            account = new ATMUser("1111111111");
        }
    }
}
