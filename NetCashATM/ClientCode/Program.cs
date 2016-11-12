
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
using NetCashATM.ATMHardware;
using BankingFramework.Interceptor_Package.Interceptors;

namespace ClientCode
{

    class Program 
    {

        private static ATMMainView _mainView;
        static ATMController controller;
        static ATMAccount account;



        class NavigationRequestInterceptor : Interceptor
        {
            PanelFactory _panelFactory = new PanelFactory();

            public void baseFunction(ContextObject e)
            {
                _mainView.SetCurrentPanel(_panelFactory.GetPanel(e.GetShortDescription()));
            }
        }
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

        
        
        [STAThread]
        public static void Main()
        {
            


            ClientRequestInterceptor myInterceptor = new ClientRequestInterceptor();
            ClientRequestDispatcher.TheInstance().RegisterClientInterceptor(myInterceptor);
            NavigationRequestInterceptor navInterceptor = new NavigationRequestInterceptor();
            NavigationRequestDispatcher.TheInstance().RegisterClientInterceptor(navInterceptor);

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

            catch (Exception e) { Debug.WriteLine("Application Exited"); }

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

            public void baseFunction(ContextObject e)
            {
                throw new NotImplementedException();
            }
        }
}
