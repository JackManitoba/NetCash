using ATMVERSION2.Controllers;
using ATMVERSION2.Models;
using ATMVERSION2.UserInterface.Panels;
using ATMVERSION2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMVERSION2
{
    public class Main
    {

        public static void main()
        {  //MainView
            ATMMainView mainView = new ATMMainView();

            //model
            ATMUser account = new ATMUser("1111111111");
            //CurrentView
            ATMPanel currentPanel = new PinPanel();
            //Controller
            PinVerificationController controller = new PinVerificationController(account, mainView);
            controller.setPanel(currentPanel);
            mainView.Activate();



        }
        
    }
}
