using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCash.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["SessionUserName"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

    }
}