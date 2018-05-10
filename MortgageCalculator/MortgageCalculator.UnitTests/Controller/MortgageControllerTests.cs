using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MortgageCalculator.UnitTests.Controller
{
    [TestClass]
    public class MortgageControllerTests
    {
        [TestMethod]
        public void GetTest()
        {
            MortgageController _MortgageController = new MortgageController();
            var res = _MortgageController.Get();
            Assert.AreNotEqual(null, res);
        }
        [TestMethod]
        public void GetbyIdTest()
        {
            MortgageController _MortgageController = new MortgageController();
            IEnumerable<MortgageCalculator.Dto.Mortgage> res = _MortgageController.Get();
            var res2 = _MortgageController.Get(res.FirstOrDefault().MortgageId);
            Assert.AreNotEqual(null, res);
            Assert.AreNotEqual(null, res2);
        }
    }
}
