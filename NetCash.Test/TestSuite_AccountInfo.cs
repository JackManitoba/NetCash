using BankingFramework.AccountManager;
using BankingFramework.FacadeClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCash.Test
{
    [TestClass]
    public class TestSuite_AccountInfo
    {
        [TestMethod]
        public void Test_AreFundsAvailable()
        {

            double withDrawelAmount = 10;
            Account UserAccount = new Account("12345678");

            bool newBalance = UserAccount.AreFundsAvailable(withDrawelAmount);

            Assert.IsTrue(newBalance);
        }

        [TestMethod]
        public void Test_IsValid_ATMAccount()
        {
            string word = "yes";
            ATMAccount UserAccount = new ATMAccount("12345678");

            bool AtmACC = UserAccount.IsValid(word);

            Assert.IsFalse(AtmACC);
        }


        [TestMethod]
        public void Test_ValidateAccount()
        {
            string word = "2222";
            ATMFacade fcd = new ATMFacade("1111111111");

            bool vcard = fcd.ValidateAccount(word);

            Assert.IsTrue(vcard);
        }
    }
}
