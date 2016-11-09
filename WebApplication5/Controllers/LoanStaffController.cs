using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetCash.Models;

namespace WebApplication5.Controllers
{
    public class LoanStaffController : Controller
    {
        // GET: StaffLoan
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoanStaffQuery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoanStaffQuery(Loan Loan)
        {
            Loan.GetLoanDataByAccountNumber();
            return View("DisplayLoan", Loan);
        }
    }
}