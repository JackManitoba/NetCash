using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;

namespace NetCashATM.Presenters
{
    public class MenuPresenter
    {
        public void NavigateToSelected(string selected)
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject(selected));
        }
    }
}
