
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
using System.IO;

namespace ClientCode
{

    class Program
    {

        private static ATMMainView _mainView;

        public int VerifiedSession = 0;

        class NavigationRequestInterceptor : Interceptor
        {
            PanelFactory _panelFactory = new PanelFactory();

            public void baseFunction(ContextObject e)
            {
                Debug.WriteLine("Program.NavigationRequestInterceptor.BaseFunction: " + e.GetShortDescription());
                _mainView.SetCurrentPanel(_panelFactory.GetPanel(e.GetShortDescription()));
            }
        }

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

        public static void SetDataPath()
        {
            var path = Path.GetFullPath(((AppDomain.CurrentDomain.BaseDirectory).Replace("\\NetCashATM\\ClientCode\\bin\\Debug", "")) + "\\NetCashWebSite\\App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }


        [STAThread]
        public static void Main()
        {
            SetDataPath();

            ClientRequestInterceptor myInterceptor = new ClientRequestInterceptor();
            ClientRequestDispatcher.TheInstance().RegisterClientInterceptor(myInterceptor);
            NavigationRequestInterceptor navInterceptor = new NavigationRequestInterceptor();
            NavigationRequestDispatcher.TheInstance().RegisterClientInterceptor(navInterceptor);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mainView = new ATMMainView();
            PinPanel pinPanel = new PinPanel();
            SetPanel(pinPanel);
            Application.Run(_mainView);
        }
    }
}
