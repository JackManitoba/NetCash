using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers.AccountManager;

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
            Account CurrentAccount = new Account(transfer.CurrentAccountNumber);
            Account TargetAccount = new Account(transfer.TargetAccountNumber);

            string Result;

            if (CurrentAccount.AreFundsAvailable(transfer.TransferAmount))
            {
                CurrentAccount.DecreaseBalance(transfer.TransferAmount);
                TargetAccount.IncreaseBalance(transfer.TransferAmount);
                Result = "You have succesfully transfered €" + transfer.TransferAmount + " to account " + transfer.TargetAccountNumber;
            }
            else Result = "You have insufficient funds for this transaction. You tried to transfer €" + transfer.TransferAmount + ". Your current balance is €" + CurrentAccount.Balance;

            Debug.WriteLine(Result);
            return View("TransferResult", (object)Result);
        }
    }
}