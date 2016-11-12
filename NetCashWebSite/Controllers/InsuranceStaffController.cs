using System.Web.Mvc;
using NetCashWebSite.Models.Insurance;

namespace NetCashWebSite.Controllers
{
    public class InsuranceStaffController : Controller
    {
        [HttpGet]
        public ActionResult InsuranceStaffQuery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsuranceStaffQuery(InsuranceQuery insuranceQuery)
        {
            if(insuranceQuery.PendingQueryExists())
            {
                insuranceQuery.GetInsuranceByAccountNumber();
                return View("ReviewQuote", insuranceQuery);
            }
            else
            {
                return View("ReviewInsuranceFailure");
            }    
        }

        public ActionResult DiscussInsurance(InsuranceQuery insuranceQuery)
        {
            insuranceQuery.MarkInsuranceAsDiscussed();
            return View();
        }
    }
}