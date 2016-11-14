using BankingFramework.FacadeClasses;
using NetCashWebSite.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NetCashWebSite.Controllers
{
    public class StatementController : Controller
    {
        [HttpGet]
        public ActionResult DisplayStatement()
        {
            WebSiteFacade webSiteFacade = new WebSiteFacade(Session["AccountNumber"].ToString());
            List<List<string>> statement = webSiteFacade.GetStatement();
            return View(statement);
        }
    }
}