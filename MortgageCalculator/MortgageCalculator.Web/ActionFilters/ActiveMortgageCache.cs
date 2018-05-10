using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MortgageCalculator.Web.ActionFilters
{
    public class ActiveMortgageCache : ActionFilterAttribute
    {
        public string CacheKey
        {
            get;
            set;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string url = filterContext.HttpContext.Request.Url.PathAndQuery;
            if (filterContext.HttpContext.Cache[this.CacheKey] != null)
            {
                filterContext.Result = (ActionResult)filterContext.HttpContext.Cache[this.CacheKey];
            }
            if (filterContext.HttpContext.Request.Url.PathAndQuery.Contains('?') && string.IsNullOrEmpty(filterContext.HttpContext.Request.Url.PathAndQuery.Split('?').LastOrDefault()))
            {
                var shot = filterContext.ActionParameters["data"] as MortgageCalculator.Web.Models.Shorting;
                var res = ((System.Web.Mvc.PartialViewResult)(filterContext.Result)).Model as IEnumerable<MortgageCalculator.Web.Models.Mortgage>;
                PartialViewResult vr = new System.Web.Mvc.PartialViewResult
                    {
                        ViewName = "~/Views/Shared/_ActiveMortgagePartial.cshtml",
                        ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                            {
                                Model = new MortgageCalculator.Web.Models.ServiceCom("").ShortData(shot, res)
                            }
                    };
                filterContext.Result = vr as ActionResult;
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var dataRefreshTime = System.Configuration.ConfigurationManager.AppSettings["MortGageDataRefreshTime"].ToString();
            DateTime now = DateTime.Now;
            DateTime dtTarget = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt16(dataRefreshTime.Split(':').FirstOrDefault()), Convert.ToInt16(dataRefreshTime.Split(':').LastOrDefault()), 0);
            if (now > dtTarget)
                dtTarget = dtTarget.AddDays(1);
            filterContext.Controller.ViewData["ExpirationTime"] = dtTarget;
            filterContext.HttpContext.Cache.Add(this.CacheKey, filterContext.Result, null, dtTarget,
                System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
            base.OnActionExecuted(filterContext);
        }
    }
}