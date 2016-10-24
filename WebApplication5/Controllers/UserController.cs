using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NetCash.Models;

namespace WebApplication5.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            //if (ModelState.IsValid)
            //{
            if (user.IsValid(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, user.RememberMe);
                Session["AccountNumber"] = user.AccountNumber;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect!");
            }
            //  }
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}