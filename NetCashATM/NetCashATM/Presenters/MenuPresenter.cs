using BankingFramework.Interceptor_Package.ContextObjects;
using BankingFramework.Interceptor_Package.Dispatchers;
using NetCashATM.UserInterface.Panels;

namespace NetCashATM.Presenters
{
    class MenuPresenter
    {
        private MainPanel _menuPanel;

        public MenuPresenter(MainPanel menuPanel)
        {
            _menuPanel = menuPanel;
        }
        
        public void NavigateToSelected(string selected)
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject(selected));
        }
    }
}
