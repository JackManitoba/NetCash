using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void Test_AllTests()
        {

            TestSuite_AccountInfo AccInfo = new TestSuite_AccountInfo();
            AccInfo.Test_AreFundsAvailable();
            AccInfo.Test_IsValid_ATMAccount();
            AccInfo.Test_ValidateAccount();

            TestSuite_Insurance Insurance_Tests = new TestSuite_Insurance();
            Insurance_Tests.Test_calculatequote_CarInsurance();
            Insurance_Tests.Test_calculatequote_HomeInsurance();
            Insurance_Tests.Test_calculatequote_PersnalInsurance();
            Insurance_Tests.Test_calculatequote_TravelInsurance();

            TestSuite_StaffInfo Staff_Info = new TestSuite_StaffInfo();
            Staff_Info.Test_IsValid_Staff();
            Staff_Info.Test_IsValid_Staff();

            TestSuite_Transaction Transaction_Test = new TestSuite_Transaction();
            Transaction_Test.Test_PerformTransaction_DEPOSIT();
            Transaction_Test.Test_PerformTransaction_WITHDRAWAL();
            Transaction_Test.Test_PerformTransaction_TRANSFER();
        }
    }
}


