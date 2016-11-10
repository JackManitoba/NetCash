using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers.AccountManager;
using Helpers.BankTransactions;

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
            transfer.CurrentAccountNumber = Session["AccountNumber"].ToString();
            Transaction Transfer = new Transfer();

            string Result;

            if (Transfer.AreFundsAvailable())
            {
                Transfer.PerformTransaction();
                Result = "You have succesfully transfered €" + transfer.TransferAmount + " to account " + transfer.TargetAccountNumber;
            }
            else Result = "You have insufficient funds for this transaction. You tried to transfer €" + transfer.TransferAmount;

            Debug.WriteLine(Result);
            return View("TransferResult", (object)Result);
        }
    }
}