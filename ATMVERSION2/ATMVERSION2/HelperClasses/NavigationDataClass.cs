using System.Collections.Generic;
using System.Diagnostics;

namespace ATMVERSION2.HelperClasses
{
    public class NavigationDataClass
    {
        protected string navigationMap;

        public NavigationDataClass()
        {
            navigationMap = "";
        }

        public void addNavigaion(string panelName)
        {
            navigationMap = panelName;
        }

        public string getNavigationPanelName()
        {
            return navigationMap;
        }

        public void setNavigationPanelName(string panelname)
        {
            navigationMap = panelname;
        }
    }
}