using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingFramework.Interceptor_Package.ContextObjects
{
   public class NavigationContextObject : ContextObject
    {
        string navigationPanelName;
        public NavigationContextObject(string navpanel) { this.navigationPanelName = navpanel; }
        public string GetObj()
        {
            return this.ToString();
        }

        public string GetShortDescription()
        {
            return this.navigationPanelName;
        }

        public string GetVerboseDescription()
        {
            return this.navigationPanelName;
        }
    }
}
