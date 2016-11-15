using System.Web.Mvc;
using NetCashWebSite.Models.Insurance;
using BankingFramework.FacadeClasses;
using System.Collections.Generic;
using System;

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
            WebSiteFacade webSiteFacade = new WebSiteFacade(insuranceQuery.AccountNumber);

            if (webSiteFacade.PendingInsuranceQueryExists())
            {
                List<string> insuranceProperties = webSiteFacade.GetInsuranceByAccountNumber();

                insuranceQuery.InsuranceTypeChoice = insuranceProperties[0];
                insuranceQuery.AgeChoice = insuranceProperties[1];
                insuranceQuery.LocationChoice = insuranceProperties[2];
                insuranceQuery.DateOfApplication = insuranceProperties[3];

                return View("ReviewQuote", insuranceQuery);
            }
            else
            {
                return View("ReviewInsuranceFailure");
            }    
        }

        public ActionResult DiscussInsurance(InsuranceQuery insuranceQuery)
        {
            WebSiteFacade webSiteFacade = new WebSiteFacade(insuranceQuery.AccountNumber);

            webSiteFacade.MarkInsuranceAsDiscussed();
            return View();
        }
    }
}