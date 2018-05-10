using MortgageCalculator.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MortgageCalculator.Web.Controllers
{
    public class HomeController : Controller
    {
        [ActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }
        [ActionName("ActiveMortgages")]
        [MortgageCalculator.Web.ActionFilters.ActiveMortgageCache(CacheKey = "ActiveMortgageCache-/Home/ActiveMortgages")]
        public ActionResult getActiveMortgages(Shorting data)
        {
            var result = new ServiceCom("api/Mortgage").getMortage(data);
            return PartialView("_ActiveMortgagePartial", result);
        }

        [ActionName("MortgageCalculator")]
        //[MortgageCalculator.Web.ActionFilters.ActiveMortgageCache(CacheKey = "ActiveMortgageCache-/Home/MortgageCalculator")]
        public ActionResult MortgageCalculator(double Principle, int TotalYears, double Premium, double IntrestRate)
        {
            var result = new MortageCalculator(Principle, TotalYears, Premium, IntrestRate).Calculate();
            return PartialView("_MortgageCalculator", result);
        }
    }
}