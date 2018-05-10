using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator.Web;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
namespace MortgageCalculator.UnitTests
{
    [TestClass]
    public class RouteConfigTest
    {
        [TestMethod]
        public void RegisterRoutesTest()
        {
            //RouteConfig.RegisterRoutes(RouteTab);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
