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

        string apply = "thank you";

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
            Insurance.SetStrategy();
            Insurance.CalculatePremium();
            return RedirectToAction("DisplayPremium", Insurance);
        }

        public ActionResult DisplayPremium(Models.Insurance Insurance)
        {
            return View(Insurance);
        }
    }
}