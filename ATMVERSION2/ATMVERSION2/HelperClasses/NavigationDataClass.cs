using System.Collections.Generic;
using System.Diagnostics;

namespace ATMVERSION2.HelperClasses
{
    public class NavigationDataClass
    {
        protected List<string> navigationMap;

        public NavigationDataClass() { navigationMap = new List<string>(10);

            for (int i = 0; i < navigationMap.Count; i++)
            {
                navigationMap[i] = "";
            }
        }

        public void addNavigaion(string panelName) { navigationMap.Add(panelName); }


        public string getNavigationPanelName(int i)
        {
            Debug.WriteLine(navigationMap[0]);
            return navigationMap[i]; }
        public void setNavigationPanelName(int i,string panelname)
        {  navigationMap.Insert(i,panelname); }


    }
}