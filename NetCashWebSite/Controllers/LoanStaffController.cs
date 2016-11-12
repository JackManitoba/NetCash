using System.Web.Mvc;
using NetCashWebSite.Models;

namespace NetCashWebSite.Controllers
{
    public class LoanStaffController : Controller
    {
        [HttpGet]
        public ActionResult LoanStaffQuery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoanStaffQuery(Loan Loan)
        {
            if(Loan.PendingApplicationExists())
            {
                Loan.GetLoanDataByAccountNumber();
                return View("DisplayLoan", Loan);
            }
            else
            {
                return View("ReviewLoanFailure", Loan);
            }
        }

        public ActionResult DiscussLoan(Loan Loan)
        {
            Loan.MarkLoanAsDiscussed();
            return View();
        }
    }
}