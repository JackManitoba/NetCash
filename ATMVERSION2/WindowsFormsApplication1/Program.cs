
using ATMVERSION2.Controllers;
using ATMVERSION2.HelperClasses;
using ATMVERSION2.Interfaces;

using ATMVERSION2.UserInterface.Panels;
using ATMVERSION2.Views;
using Helpers.AccountManager;
using Helpers.Interceptor_Package;
using Helpers.Interceptor_Package.Dispatchers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        static ATMController controller;
        static ATMAccount account;
        [STAThread]
        public static void Main()
        {
             ClientRequestInterceptor myInterceptor = new ClientRequestInterceptor();
            ClientRequestDispatcher.theInstance().registerClientInterceptor(myInterceptor);
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
            controller.insertCard("");
            controller.startATM();
            // mainView.Activate();
            mainView.registerObserver(new Program());

            try
            {
                Application.Run(mainView);
            }
            catch (Exception e) { Debug.WriteLine("Application Exited"); }
        }

        public void update()
        {
        }

        public void update(Subject e)
        {
            controller.handleChange(e);
        }
        
        
    }
}
