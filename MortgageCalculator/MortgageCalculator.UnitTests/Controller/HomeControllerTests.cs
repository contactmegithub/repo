using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator.Web.Controllers;
using MortgageCalculator.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MortgageCalculator.UnitTests.Controller
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexTest()
        {
            HomeController _HomeController = new HomeController();
            ViewResult result = _HomeController.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
        [TestMethod]
        public void getActiveMortgagesTest()
        {
            HomeController _HomeController = new HomeController();
            ViewResult result = _HomeController.getActiveMortgages(new Shorting() { ordershortBy = "1", orderThenBy = "1", shortBy = "1", thenBy = "2" }) as ViewResult;
            Assert.AreEqual("_ActiveMortgagePartial", result.ViewName);
        }
        [TestMethod]
        public void MortgageCalculatorTest()
        {
            double Principle = 100000, Premium = 0, IntrestRate = 12;
            int TotalYears = 10;
            HomeController _HomeController = new HomeController();
            ViewResult result = _HomeController.MortgageCalculator(Principle, TotalYears, Premium, IntrestRate) as ViewResult;
            Assert.AreEqual("_MortgageCalculator", result.ViewName);
        }
    }
}
