using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingFramework.InterceptorPackage.ContextObjects
{
   public class NavigationContextObject : ContextObject
    {
        private string _navigationPanelName;

        public NavigationContextObject(string navPanelName)
        {
            _navigationPanelName = navPanelName;
        }

        public string GetObj()
        {
            return ToString();
        }

        public string GetShortDescription()
        {
            return _navigationPanelName;
        }

        public string GetVerboseDescription()
        {
            return this._navigationPanelName;
        }

        public void Service()
        {
            //Not Implemented
        }
    }
}
