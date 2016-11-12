﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankingFramework.AccountManager;
using BankingFramework.BankTransactions;
using BankingFramework.FacadeClasses;
using NetCashWebSite.Models.Insurance;



namespace NetCash.Test
{
    [TestClass]
    public class TestSuite

    {
        [TestMethod]
        public void TestMethod1()
        {

            double withDrawelAmount = 10;
            Account UserAccount = new Account("12345678");

            bool newBalance = UserAccount.AreFundsAvailable(withDrawelAmount);

            Assert.IsTrue(newBalance);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string word = "yes";
            ATMAccount UserAccount = new ATMAccount("12345678");

            bool AtmACC = UserAccount.IsValid(word);

            Assert.IsFalse(AtmACC);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string word = "DEPOSIT";
            Account acc = new Account("12345678");
            double startBalance = acc.Balance;
            double amountToDeposit = 20;

            Deposit dep = new Deposit(acc, word, amountToDeposit);
            dep.PerformTransaction();

            double expectedBalance = startBalance + amountToDeposit;
            Account accAfter = new Account("12345678");
            Assert.AreEqual(expectedBalance, accAfter.Balance);

        }
        [TestMethod]
        public void TestMethod4()
        {
            string word = "WITHDRAWEL";
            Account acc = new Account("12345678");
            double startBalance = acc.Balance;
            double amountToWithDraw = 20;

            Withdrawal wd = new Withdrawal(acc, word, amountToWithDraw);
            wd.PerformTransaction();

            double expectedBalance = startBalance - amountToWithDraw;
            Account accAfter = new Account("12345678");
            Assert.AreEqual(expectedBalance, accAfter.Balance);
        }

        [TestMethod]
        public void TestMethod5()
        {
            string word = "0216";
            ATMFacade fcd = new ATMFacade("1111111111");

            bool vcard = fcd.ValidateAccount(word);

            Assert.IsTrue(vcard);
        }
        [TestMethod]
        public void TestMethod6()
        {
            string word = "1710";
            ATMFacade fcd = new ATMFacade("1111111111");
            string retAccBal = fcd.ReturnAccountBalance();

            Assert.AreEqual(word, retAccBal);
        }
        [TestMethod]
        public void TestMethod7()
        {
            int age = 20;
            int location = 2;
            Car_Insurance car_i = new Car_Insurance();
            double calc_amnt = 2200;
            double total = car_i.calculatequote(age, location);

            Assert.AreEqual(total, calc_amnt);

        }
        [TestMethod]
        public void TestMethod8()
        {
            int age = 20;
            int location = 2;
            Travel_Insurance jrn_i = new Travel_Insurance();
            double calc_amnt = 1100;
            double total = jrn_i.calculatequote(age, location);

            Assert.AreEqual(total, calc_amnt);

        }
        [TestMethod]
        public void TestMethod9()
        {
            int age = 20;
            int location = 2;
            Persnal_Insurance pers_i = new Persnal_Insurance();
            double calc_amnt = 6600;
            double total = pers_i.calculatequote(age, location);

            Assert.AreEqual(total, calc_amnt);

        }
        [TestMethod]
        public void TestMethod10()
        {
            int age = 20;
            int location = 2;
            Home_insurance house_i = new Home_insurance();
            double calc_amnt = 8800;
            double total = house_i.calculatequote(age, location);

            Assert.AreEqual(total, calc_amnt);

        }
        [TestMethod]
        public void TestMethod11()
        { 
            string frmAcc = "12345678";
            string toAcc = "12345555";
            string desc = "Transfer";
            double amnt = 50;


        }

    }
}

