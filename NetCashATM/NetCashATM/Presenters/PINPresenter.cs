using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCashATM.UserInterface.Panels;
using BankingFramework.FacadeClasses;
using System.Data.Common;
using System.Configuration;

namespace NetCashATM.Presenters
{
    public class PINPresenter
    {
        private PinPanel _pinPanel;
        private ATMFacade _atmFacade;

        public PINPresenter(PinPanel pinPanel)
        {
            _pinPanel = pinPanel;
        }

        public ATMPanel GetPinPanel()
        {
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
            return _pinPanel;
        }

        public ATMPanel GetPinPanel(string pin)
        {
            if(_atmFacade.ValidateAccount(pin))
            {
                return
            }
            return new PinRetryPanel;
        }
    }
}
