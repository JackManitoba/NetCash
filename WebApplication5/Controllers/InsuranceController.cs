using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCash.Controllers
{
    public class InsuranceController : Controller
    {
        // GET: Insurance
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InsuranceCustomer()
        {
            return View(new Models.Insurance());
        }

        [HttpPost]
        public ActionResult InsuranceCustomer(Models.Insurance Insurance)
        {
            if(Insurance.PendingQueryExists(Session["AccountNumber"]))
            {
                return View("InsuranceFailure");               
            }
            else
            {
                Insurance.SubmitApplication(Session["AccountNumber"].ToString());
                Insurance.SetStrategy();
                Insurance.CalculatePremium();
                return View("DisplayPremium", Insurance);
            }
            
        }

        public ActionResult DisplayPremium(Models.Insurance Insurance)
        {
            return View(Insurance);
        }
    }
}