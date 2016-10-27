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
            return View();


        }
    }
}