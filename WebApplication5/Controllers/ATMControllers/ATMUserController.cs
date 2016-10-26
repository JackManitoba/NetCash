using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models.ATMModels;

namespace WebApplication5.Controllers.ATMControllers
{
    public class ATMUserController : Controller
    {
        // GET: ATMUser
        
        public ActionResult ATMLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ATMLogin(ATMUser user)
        {
            //if (ModelState.IsValid)
            //{
            if (user.IsValid(user.cardNumber, user.PIN))
            {   user.setBalance();
                Debug.WriteLine("Account number = " + user.accountNumber);
                Debug.WriteLine("Card number = " + user.cardNumber);
                Debug.WriteLine("Card Pin = "+ user.PIN);
                Debug.WriteLine("Account Balance = " + user.balance);

                Session["AccountNumber"] = user.accountNumber;
                return RedirectToAction("Index", "ATMHome",user);
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect!");
            }
            //  }
            return View(user);
        }
    }
}