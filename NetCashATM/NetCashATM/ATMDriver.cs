using NetCashATM.UserInterface.Panels;
using NetCashATM.Views;
using System;
using System.Windows.Forms;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Interceptors;
using BankingFramework.InterceptorPackage.Dispatchers;

namespace ClientCode
{
    class Program
    {
        private static ATMMainView _mainView;

        class NavigationRequestInterceptor : Interceptor
        {
            PanelFactory _panelFactory = new PanelFactory();

            public void ConsumeService(ContextObject e)
            {
                _mainView.SetCurrentPanel(_panelFactory.GetPanel(e.GetShortDescription()));
            }
        }

        [STAThread]
        public static void Main()
        {
            SetDataPath();

            LoggingInfoInterceptor myInterceptor = new LoggingInfoInterceptor();
            LoggingInfoDispatcher.TheInstance().RegisterClientInterceptor(myInterceptor);

            NavigationRequestInterceptor navInterceptor = new NavigationRequestInterceptor();
            NavigationRequestDispatcher.TheInstance().RegisterClientInterceptor(navInterceptor);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mainView = new ATMMainView();
            PinPanel pinPanel = new PinPanel();
            SetPanel(pinPanel);

            Application.Run(_mainView);
        }

        public static void SetPanel(ATMPanel currentPanel)
        {
            _mainView.SetCurrentPanel(currentPanel);
        }

        public static void SetDataPath()
        {
            var path = (AppDomain.CurrentDomain.BaseDirectory);
            int position = path.IndexOf("NetCash");
            var substring = path.Substring(0, position);
            path = substring + "NetCash\\NetCashWebSite\\App_Data";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }
    }
}
