using System.Collections.Generic;
using System.Diagnostics;

namespace NetCashATM.HelperClasses
{
    public class NavigationDataClass
    {
        private string _navigationMap;

        public NavigationDataClass()
        {
            _navigationMap = "";
        }

        public void AddNavigaion(string panelName)
        {
            _navigationMap = panelName;
        }

        public string GetNavigationPanelName()
        {
            return _navigationMap;
        }

        public void SetNavigationPanelName(string panelname)
        {
            _navigationMap = panelname;
        }
    }
}