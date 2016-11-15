using BankingFramework.FacadeClasses;
using NetCashWebSite.Models;
using System.Web.Mvc;

namespace NetCashWebSite.Controllers
{
    public class LoanController : Controller
    {
        [HttpGet]
        public ActionResult LoanApplication()
        {
            return View(new Loan());
        }

        [HttpPost]
        public ActionResult LoanApplication(Loan LoanApplication)
        {
            WebSiteFacade webSiteFacade = new WebSiteFacade(Session["AccountNumber"].ToString());

            if (webSiteFacade.PendingApplicationExists())
            {
                return View("LoanFailure");
            }
            else
            {
                webSiteFacade.SubmitLoanApplication(LoanApplication.LoanChoice, LoanApplication.AmountRequired, LoanApplication.PeriodOfRepayment);
                return View("LoanResult");
            }
        }    
    }
}