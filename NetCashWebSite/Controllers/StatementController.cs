using BankingFramework.FacadeClasses;
using NetCashWebSite.Models;
using System.Web.Mvc;

namespace NetCashWebSite.Controllers
{
    public class StatementController : Controller
    {
        [HttpGet]
        public ActionResult PrintStatement()
        {
            WebSiteFacade webSiteFacade = new WebSiteFacade(Session["AccountNumber"].ToString());
            Statement statement = webSiteFacade.GetStatement();
            return View(statement);
        }
    }
}