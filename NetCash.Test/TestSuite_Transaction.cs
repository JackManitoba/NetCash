using BankingFramework.AccountManager;
using BankingFramework.BankTransactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetCash.Test
{
    [TestClass]
    public class TestSuite_Transaction
    {
        [TestMethod]
        public void Test_PerformTransaction_DEPOSIT()
        {
            string word = "DEPOSIT";
            Account acc = new Account("12345678");
            double startBalance = acc.GetBalance();
            double amountToDeposit = 70;

            Deposit dep = new Deposit(acc, word, amountToDeposit);
            dep.PerformTransaction();

            double expectedBalance = startBalance + amountToDeposit;
            Account accAfter = new Account("12345678");
            Assert.AreEqual(expectedBalance, accAfter.GetBalance());

        }
        [TestMethod]
        public void Test_PerformTransaction_WITHDRAWAL()
        {
            string word = "WITHDRAWAL";
            Account acc = new Account("12345678");
            double startBalance = acc.GetBalance();
            double amountToWithDraw = 20;

            Withdrawal wd = new Withdrawal(acc, word, amountToWithDraw);
            wd.PerformTransaction();

            double expectedBalance = startBalance - amountToWithDraw;
            Account accAfter = new Account("12345678");
            Assert.AreEqual(expectedBalance, accAfter.GetBalance());
        }
        [TestMethod]
        public void Test_PerformTransaction_TRANSFER()
        {
            string desc = "TRANSFER";
            double amnt = 50;
            Account outAcc = new Account("12345678");
            Account inAcc = new Account("12345555");


            Transfer trf = new Transfer(outAcc.GetAccountNumber(), inAcc.GetAccountNumber(), desc, amnt);
            trf.PerformTransaction();

            double expectedOutgoingBalance = outAcc.GetBalance();
            double expectedIncomingBalance = inAcc.GetBalance();
            Account NewOutAcc = new Account("12345678");
            Account NewInAcc = new Account("12345555");


            Assert.AreEqual(NewOutAcc.GetBalance(), expectedOutgoingBalance);
            Assert.AreEqual(NewInAcc.GetBalance(), expectedIncomingBalance);
        }
    }
}
