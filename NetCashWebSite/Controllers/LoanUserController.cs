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
            if(LoanApplication.PendingApplicationExists(Session["AccountNumber"].ToString()))
            {
                return View("LoanFailure");
            }
            else
            {
                LoanApplication.SubmitApplication(Session["AccountNumber"]);
                return View("LoanResult");
            }
        }    
    }
}