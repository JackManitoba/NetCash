using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models.ATMModels;

namespace WebApplication5.Controllers.ATMControllers
{
    public class ATMFunctionsController : Controller
    {
        static ATMUser user = null;
        // GET: ATMFunctions
        public ActionResult Withdrawal(ATMUser u)
        {
            user = u;
            return View();
        }

        [HttpPost]
        public ActionResult Withdrawal(ATMTransaction t)
        {
            t.type = "Withdrawal";
            t.accountNumber = Session["accountNumber"].ToString();
            if (user.balance >= t.amount)
            {
                user.executeATMTransaction(t);
                return RedirectToAction("Index", "ATMHome", user);
            }

            else
            {
                Debug.WriteLine("Not Enough Money in account for withdrawal");
                return View();
            }
        }
        public ActionResult Deposit(ATMUser u)
        {
            user = u;
           
            return View();
        }
        
        [HttpPost]
        public ActionResult Deposit(ATMTransaction t)
        {
        
            t.type = "Deposit";
            t.accountNumber = Session["accountNumber"].ToString();
            user.executeATMTransaction(t);
            return RedirectToAction("Index", "ATMHome", user);
        }
        public ActionResult AccountInfo(ATMUser u)
        {
            user = u;
            return View(user);
        }
        /*
        [HttpPost]
        public ActionResult AccountInfo()
        {
            return View();
        }*/
    }
}