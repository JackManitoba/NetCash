using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers.AccountManager;
using Helpers.BankTransactions;
using Helpers.FacadeClasses;

namespace NetCash.Controllers
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
        public ActionResult Transfer(Models.Transfer transfer)
        {
            OnlineAppFacade onlineAppFacade = new OnlineAppFacade(Session["AccountNumber"].ToString());

            string Result;

            if (onlineAppFacade.areFundsAvailable(transfer.TransferAmount))
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