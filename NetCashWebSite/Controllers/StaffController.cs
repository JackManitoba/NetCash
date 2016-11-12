using System.Web.Mvc;
using System.Web.Security;
using NetCashWebSite.Models;

namespace NetCashWebSite.Controllers
{
    public class StaffController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Staff staff)
        {
            if (ModelState.IsValid)
            {
                if (staff.IsValid(staff.UserName, staff.Password))
                {
                    FormsAuthentication.SetAuthCookie(staff.UserName, false);
                    Session["SessionRole"] = "BankStaff";
                    Session["SessionUserName"] = staff.UserName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is incorrect.");
                }
            }
            return View(staff);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}