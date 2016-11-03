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

            if(CurrentAccount.AreFundsAvailable(transfer.TransferAmount))
            {
                CurrentAccount.UpdateAmount(-transfer.TransferAmount);
                TargetAccount.UpdateAmount(transfer.TransferAmount);
            }
            return View();
        }
    }
}