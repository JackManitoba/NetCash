using NetCashWebSite.Models.Insurance;
using System.Web.Mvc;

namespace NetCashWebSite.Controllers
{
    public class InsuranceController : Controller
    {
        [HttpGet]
        public ActionResult InsuranceCustomer()
        {
            return View(new InsuranceQuery());
        }

        [HttpPost]
        public ActionResult InsuranceCustomer(InsuranceQuery insuranceQuery)
        {
            if(insuranceQuery.PendingQueryExists(Session["AccountNumber"]))
            {
                return View("InsuranceFailure");               
            }
            else
            {
                insuranceQuery.SubmitApplication(Session["AccountNumber"].ToString());
                insuranceQuery.SetStrategy();
                insuranceQuery.CalculatePremium();
                return View("DisplayPremium", insuranceQuery);
            }        
        }

        public ActionResult DisplayPremium(InsuranceQuery insuranceQuery)
        {
            return View(insuranceQuery);
        }
    }
}