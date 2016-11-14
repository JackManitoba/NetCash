using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;

using System.Diagnostics;

namespace NetCashATM.Presenters
{
    class MenuPresenter
    {
        

        public MenuPresenter()
        {
         
        }
        
        public void NavigateToSelected(string selected)
        {

            Debug.WriteLine("MenuPrsenter.NavigateToSelected(): " + selected);
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject(selected));
        }
    }
}
