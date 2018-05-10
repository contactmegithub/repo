using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator.Web.Models;

namespace MortgageCalculator.UnitTests
{
    [TestClass]
    public class MortgageTests
    {
        [TestMethod]
        public void CalculateTest()
        {
            double Principle = 100000, Premium = 0, IntrestRate = 12;
            int TotalYears = 10;
            MortageCalculator _MortageCalculator = new MortageCalculator(Principle, TotalYears, Premium, IntrestRate);
            var result = _MortageCalculator.Calculate();
            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void getMortageTest()
        {
            ServiceCom _ServiceCom = new ServiceCom("api/Mortgage");
            var result = _ServiceCom.getMortage(new Shorting() { ordershortBy = "1", orderThenBy = "1", shortBy = "1", thenBy = "2" });
            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void ShortDataTest()
        {
            ServiceCom _ServiceCom = new ServiceCom("api/Mortgage");
            var result = _ServiceCom.getMortage(new Shorting() { ordershortBy = "1", orderThenBy = "1", shortBy = "1", thenBy = "2" });
            var res = _ServiceCom.getMortage();
            var actualResult = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "1", shortBy = "1", thenBy = "2" }, result);
            Assert.AreNotEqual(null, actualResult);
            var actualResult2 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "1", shortBy = "1", thenBy = "1" }, result);
            var actualResult3 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "1", shortBy = "1", thenBy = "2" }, result);
            var actualResult4 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "1", shortBy = "2", thenBy = "1" }, result);
            var actualResult5 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "1", shortBy = "2", thenBy = "2" }, result);
            var actualResult21 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "1", shortBy = "1", thenBy = "2" }, result);
            var actualResult6 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "2", shortBy = "1", thenBy = "1" }, result);

            var actualResult7 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "0", orderThenBy = "0", shortBy = "0", thenBy = "0" }, result);
            var actualResult8 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "0", shortBy = "1", thenBy = "0" }, result);
            var actualResult9 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "2", orderThenBy = "0", shortBy = "1", thenBy = "0" }, result);
            var actualResult10 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "0", shortBy = "2", thenBy = "0" }, result);
            var actualResult11 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "2", orderThenBy = "0", shortBy = "2", thenBy = "0" }, result);
            var actualResult12 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "0", orderThenBy = "1", shortBy = "0", thenBy = "1" }, result);
            var actualResult13 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "0", orderThenBy = "2", shortBy = "0", thenBy = "1" }, result);
            var actualResult14 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "0", orderThenBy = "1", shortBy = "0", thenBy = "2" }, result);
            var actualResult15 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "0", orderThenBy = "2", shortBy = "0", thenBy = "2" }, result);

            var actualResult16 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "2", shortBy = "1", thenBy = "2" }, result);
            var actualResult17 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "2", orderThenBy = "1", shortBy = "1", thenBy = "2" }, result);
            var actualResult18 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "1", shortBy = "2", thenBy = "1" }, result);
            var actualResult19 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "2", orderThenBy = "0", shortBy = "2", thenBy = "1" }, result);
            var actualResult20 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "1", orderThenBy = "2", shortBy = "1", thenBy = "2" }, result);
            var actualResult1221 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "2", orderThenBy = "1", shortBy = "1", thenBy = "2" }, result);
            var actualResult1222 = _ServiceCom.ShortData(new Shorting() { ordershortBy = "2", orderThenBy = "2", shortBy = "1", thenBy = "2" }, result);

        }
        [TestMethod]
        public void isLoanCompletedTest()
        {
            double Principle = 100000, Premium = 0, IntrestRate = 12;
            int TotalYears = 10;
            MortageCalculator _MortageCalculator = new MortageCalculator(Principle, TotalYears, Premium, IntrestRate);
            PrivateObject prObj = new PrivateObject(_MortageCalculator);
            var res = prObj.Invoke("isLoanCompleted");
            Assert.AreNotEqual(null, res);
            Principle = 0;
            _MortageCalculator = new MortageCalculator(0, TotalYears, Premium, IntrestRate);
            var res2 = prObj.Invoke("isLoanCompleted");
            Assert.AreNotEqual(null, res2);
            _MortageCalculator = new MortageCalculator(0, TotalYears, Premium, IntrestRate);
            var res3 = prObj.Invoke("isLoanCompleted");
            Assert.AreNotEqual(null, res3);
        }
        [TestMethod]
        public void calculatePremiumTest()
        {
            double Principle = 100000, Premium = 0, IntrestRate = 12;
            int TotalYears = 10;
            MortageCalculator _MortageCalculator = new MortageCalculator(Principle, TotalYears, Premium, IntrestRate);
            PrivateObject prObj = new PrivateObject(_MortageCalculator);
            var res = prObj.Invoke("calculatePremium");
            Assert.AreNotEqual(null, res);
        }
        [TestMethod]
        public void GetInterestTest()
        {
            double Principle = 100000, Premium = 0, IntrestRate = 12;
            int TotalYears = 10;
            MortageCalculator _MortageCalculator = new MortageCalculator(Principle, TotalYears, Premium, IntrestRate);
            PrivateObject prObj = new PrivateObject(_MortageCalculator);
            var res = prObj.Invoke("GetInterest");
            Assert.AreNotEqual(null, res);
        }
        [TestMethod]
        public void GetPrincipalPaidTest()
        {
            double Principle = 100000, Premium = 0, IntrestRate = 12;
            int TotalYears = 10;
            MortageCalculator _MortageCalculator = new MortageCalculator(Principle, TotalYears, Premium, IntrestRate);
            PrivateObject prObj = new PrivateObject(_MortageCalculator);
            var res = prObj.Invoke("GetPrincipalPaid");
            Assert.AreNotEqual(null, res);
        }
        [TestMethod]
        public void OutstandingLoanBalanceTest()
        {
            double Principle = 100000, Premium = 0, IntrestRate = 12;
            int TotalYears = 10;
            MortageCalculator _MortageCalculator = new MortageCalculator(Principle, TotalYears, Premium, IntrestRate);
            PrivateObject prObj = new PrivateObject(_MortageCalculator);
            var res = prObj.Invoke("OutstandingLoanBalance");
            Assert.AreNotEqual(null, res);
        }
    }
}
