using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using NetCashATM.UserInterface.Panels;
using System.Diagnostics;

namespace NetCashATM.Presenters
{
    class MenuPresenter
    {
        private MainPanel _menuPanel;

        public MenuPresenter(MainPanel menuPanel)
        {
            Debug.WriteLine("MenuPrsenter.MenuPresenter()");
            _menuPanel = menuPanel;
        }
        
        public void NavigateToSelected(string selected)
        {

            Debug.WriteLine("MenuPrsenter.NavigateToSelected(): " + selected);
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject(selected));
        }
    }
}
