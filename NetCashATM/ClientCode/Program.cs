
using NetCashATM.Controllers;
using NetCashATM.Interfaces;

using NetCashATM.UserInterface.Panels;
using NetCashATM.Views;
using BankingFramework.AccountManager;
using BankingFramework.Interceptor_Package;
using BankingFramework.Interceptor_Package.Dispatchers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using NetCashATM.Views;
using NetCashATM.ATMHardware;
using NetCashATM.Presenters;

namespace ClientCode
{

    class Program : Observer
    {
        public int VerifiedSession = 0;
        public List<Subject> SubjectList
        {
            get
            {
                return SubjectList;
            }
        }

        public static void SetPanel(ATMPanel currentPanel)
        {
            _mainView.SetCurrentPanel(currentPanel);
        }

        private static ATMMainView _mainView;
        static ATMController controller;
        static ATMAccount account;
        
        [STAThread]
        public static void Main()
        {

            ClientRequestInterceptor myInterceptor = new ClientRequestInterceptor();
            ClientRequestDispatcher.TheInstance().RegisterClientInterceptor(myInterceptor);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mainView = new ATMMainView();
            SetPanel(new PinPanel());
            Application.Run(_mainView);
            // mainView.Activate();
            //_mainView.RegisterObserver(new Program());

            try
            {
                Application.Run(_mainView);
            }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
            catch (Exception e) { Debug.WriteLine("Application Exited"); }
#pragma warning restore CS0168 // The variable 'e' is declared but never used
        }

        public void Update()
        {
        }

        public static string InsertCard(string cardLocation)
        {
            CardReader cardReader = new CardReader(cardLocation);
            return cardReader.GetCardNumber();
        }

        public void Update(Subject e)
        {
            controller.HandleChange(e);
        }
        
        
    }
}
