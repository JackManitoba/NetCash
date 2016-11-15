using BankingFramework.FacadeClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCashWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCash.Test
{
    [TestClass]
    public class TestSuite_StaffInfo
    {        
        [TestMethod]
        public void Test_PendingApplicationExists()
        {
            string AccNum = "12345678";
            WebSiteFacade webSiteFacade = new WebSiteFacade(AccNum);

            bool LoanAppCheck = webSiteFacade.PendingApplicationExists();

            Assert.IsTrue(LoanAppCheck);

        }
        [TestMethod]
        public void Test_IsValid_Staff()
        {
            Staff staffGuy = new Staff();
            string staffname = "dermot";
            string password = "12345";

            bool vstaff = staffGuy.IsValid(staffname, password);

            Assert.IsTrue(vstaff);
        }
    }
}
