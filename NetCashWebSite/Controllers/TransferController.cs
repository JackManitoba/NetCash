using System.Diagnostics;
using System.Web.Mvc;
using BankingFramework.FacadeClasses;
using NetCashWebSite.Models;

namespace NetCashWebSite.Controllers
{
    public class TransferController : Controller
    {
        // GET: Transfer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Transfer(Transfer transfer)
        {
            WebSiteFacade onlineAppFacade = new WebSiteFacade(Session["AccountNumber"].ToString());

            string Result;

            if (onlineAppFacade.AreFundsAvailable(transfer.TransferAmount))
            {
                onlineAppFacade.PerformTransaction(transfer.TargetAccountNumber, transfer.TransferAmount);
                Result = "You have succesfully transfered €" + transfer.TransferAmount + " to account " + transfer.TargetAccountNumber;
            }
            else Result = "You have insufficient funds for this transaction. You tried to transfer €" + transfer.TransferAmount;

            Debug.WriteLine(Result);
            return View("TransferResult", (object)Result);
        }
    }
}