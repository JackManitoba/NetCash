using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models.ATMModels;

namespace WebApplication5.Controllers.ATMControllers
{
    public class ATMHomeController : Controller
    {
        // GET: ATMHome
        public ActionResult Index(ATMUser user)
        {
            Debug.WriteLine("DEPOSIT : User account" + user.accountNumber);
            Debug.WriteLine("DEPOSIT : User account" + user.balance);
            Debug.WriteLine("DEPOSIT : User account" + user.cardNumber);
            Debug.WriteLine("DEPOSIT : User account" + user.PIN);
            Session["accountNumber"] = user.accountNumber.ToString();
            return View(user);
            
        }
    }
}