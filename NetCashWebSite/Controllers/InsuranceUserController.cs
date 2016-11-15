using BankingFramework.FacadeClasses;
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
            WebSiteFacade webSiteFacade = new WebSiteFacade(Session["AccountNumber"].ToString());

            if (webSiteFacade.PendingInsuranceQueryExists())
            {
                return View("InsuranceFailure");               
            }
            else
            {
                webSiteFacade.SubmitInsuranceQuery(insuranceQuery.InsuranceTypeChoice, insuranceQuery.AgeChoice, insuranceQuery.LocationChoice);
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