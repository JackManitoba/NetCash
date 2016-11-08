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
                if ( Session["SessionRole"] != null){

                if (Session["SessionRole"].ToString() == "BankStaff")
                {

                    return RedirectToAction("StaffHomepage");
                }
                else 
                {

                    return RedirectToAction("UserHomePage");
                }
            }
               else

               {
                   return View();
               }
            
        }
        public ActionResult StaffHomepage()
        {
            return View();
        }

        public ActionResult UserHomePage()
        {
            return View();
        }
    }
}