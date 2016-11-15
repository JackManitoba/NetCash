using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCashWebSite.Models.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCash.Test
{
    [TestClass]
    public class TestSuite_Insurance
    {
        [TestMethod]
        public void Test_calculatequote_CarInsurance()
        {
            int age = 20;
            int location = 2;
            Car_Insurance car_i = new Car_Insurance();
            double calc_amnt = 2200;
            double total = car_i.calculatequote(age, location);

            Assert.AreEqual(total, calc_amnt);

        }
        [TestMethod]
        public void Test_calculatequote_TravelInsurance()
        {
            int age = 20;
            int location = 2;
            Travel_Insurance jrn_i = new Travel_Insurance();
            double calc_amnt = 1100;
            double total = jrn_i.calculatequote(age, location);

            Assert.AreEqual(total, calc_amnt);

        }
        [TestMethod]
        public void Test_calculatequote_PersnalInsurance()
        {
            //testclass intentionally spelled wrong to match class name
            int age = 20;
            int location = 2;
            Persnal_Insurance pers_i = new Persnal_Insurance();
            double calc_amnt = 6600;
            double total = pers_i.calculatequote(age, location);

            Assert.AreEqual(total, calc_amnt);

        }
        [TestMethod]
        public void Test_calculatequote_HomeInsurance()
        {
            int age = 20;
            int location = 2;
            Home_insurance house_i = new Home_insurance();
            double calc_amnt = 8800;
            double total = house_i.calculatequote(age, location);

            Assert.AreEqual(total, calc_amnt);

        }
    }
}
