using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetCash.Models;

namespace WebApplication5.Controllers
{
    public class Insurance_StaffController : Controller
    {
        // GET: Insurance_Staff
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Insurance_StaffQuery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insurance_StaffQuery(Insurance insurance)
        {
            insurance.GetInsuranceByAccountNumber();
            return View("DiscussInsurance", insurance);
        }
        public ActionResult DiscussInsurance(Insurance insurance)
        {
            insurance.MarkInsuranceAsDiscussed();
            return View();
        }

    }
}