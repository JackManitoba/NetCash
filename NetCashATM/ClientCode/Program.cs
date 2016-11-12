
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


        static ATMMainView mainView;
        static ATMController controller;
#pragma warning disable CS0649 // Field 'Program.account' is never assigned to, and will always have its default value null
        static ATMAccount account;
#pragma warning restore CS0649 // Field 'Program.account' is never assigned to, and will always have its default value null
        [STAThread]
        public static void Main()
        {

            ClientRequestInterceptor myInterceptor = new ClientRequestInterceptor();
            ClientRequestDispatcher.TheInstance().RegisterClientInterceptor(myInterceptor);

            Debug.WriteLine("Dispatcher and interceptor created and connected");




            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //MainView
            mainView = new ATMMainView();

            //model
           

            //CurrentView
            ATMPanel currentPanel = new PinPanel();
            //Controller
           
            controller = new ATMController(account, mainView);
            controller.InsertCard("");
            controller.StartATM();
            // mainView.Activate();
            mainView.RegisterObserver(new Program());

            try
            {
                Application.Run(mainView);
            }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
            catch (Exception e) { Debug.WriteLine("Application Exited"); }
#pragma warning restore CS0168 // The variable 'e' is declared but never used
        }

        public void Update()
        {
        }

        public void Update(Subject e)
        {
            controller.HandleChange(e);
        }
        
        
    }
}
