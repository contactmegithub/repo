using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MortgageCalculator.Web.Models
{
    public class ListMortgage
    {
        public List<Mortgage> _Mortgage { get; set; }
    }
    public class Mortgage
    {
        public int MortgageId { get; set; }
        public string Name { get; set; }
        public MortgageType MortgageType { get; set; }
        public InterestRepayment InterestRepayment { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public int TermsInMonths { get; set; }
        public decimal CancellationFee { get; set; }
        public decimal EstablishmentFee { get; set; }
        public Guid SchemaIdentifier { get; internal set; }
        public decimal InterestRate { get; set; }
    }

    public enum MortgageType
    {
        Variable,
        Fixed
    }

    public enum InterestRepayment
    {
        InterestOnly,
        PrincipalAndInterest
    }

    #region Calculator
    public class Result
    {
        public double TotalRepayment { get; set; }
        public double TotalIntrestPaid { get; set; }
        public IEnumerable<YResult> YResult { get; set; }
    }
    public class YResult
    {
        public int Year { get; set; }
        public double InterestPaid { get; set; }
        public double PrincipalPaid { get; set; }
        public double OutstandingLoanBalance { get; set; }
        public IEnumerable<MResult> MResult { get; set; }
    }
    public class MResult
    {
        public string CurrentMonth { get; set; }
        public double InterestPaid { get; set; }
        public double PrincipalPaid { get; set; }
        public double OutstandingLoanBalance { get; set; }
    }
    #endregion
    public class Shorting
    {
        public string shortBy { get; set; }
        public string ordershortBy { get; set; }
        public string thenBy { get; set; }
        public string orderThenBy { get; set; }
    }

    public class ServiceCom
    {
        private string Url = string.Empty;
        private string baseUrl = "http://localhost:49608/";
        public ServiceCom(string Url)
        {
            this.Url = Url;
        }
        public IEnumerable<Mortgage> getMortage(Shorting data = null)
        {
            List<Mortgage> _Mortgage = new List<Mortgage>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(this.Url).Result;
                if (!response.IsSuccessStatusCode)
                    return null;
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                _Mortgage = JsonConvert.DeserializeObject<List<Mortgage>>(responseContent.ReadAsStringAsync().Result);
                return data == null ? _Mortgage.AsEnumerable() : ShortData(data, _Mortgage.AsEnumerable());
            }
        }

        public IEnumerable<Mortgage> ShortData(Shorting data, IEnumerable<Mortgage> _Mortgage)
        {
            return _Mortgage.ToList().AsEnumerable();
            //string option = string.Empty;
            //if (!string.IsNullOrEmpty(data.shortBy) && !string.IsNullOrEmpty(data.thenBy) && data.shortBy.Equals(data.thenBy))
            //    data.orderThenBy = "0";
            //if (!string.IsNullOrEmpty(data.shortBy) && !string.IsNullOrEmpty(data.ordershortBy))
            //{
            //    if (!data.shortBy.Equals("0") && !data.ordershortBy.Equals("0"))
            //    {
            //        option = data.shortBy + data.shortBy;
            //    }
            //}
            //if (!string.IsNullOrEmpty(data.thenBy) && !string.IsNullOrEmpty(data.orderThenBy))
            //{
            //    if (!data.thenBy.Equals("0") && !data.orderThenBy.Equals("0"))
            //    {
            //        option = data.thenBy + data.orderThenBy;
            //    }
            //}
            //switch (option)
            //{
            //    case "11":
            //        return _Mortgage.OrderBy(x => x.MortgageType);
            //    case "12":
            //        return _Mortgage.OrderByDescending(x => x.MortgageType);
            //    case "21":
            //        return _Mortgage.OrderBy(x => x.InterestRate);
            //    case "22":
            //        return _Mortgage.OrderByDescending(x => x.InterestRate);
            //    case "1121":
            //        return _Mortgage.OrderBy(x => x.MortgageType).ThenBy(x => x.InterestRate);
            //    case "1122":
            //        return _Mortgage.OrderBy(x => x.MortgageType).ThenByDescending(x => x.InterestRate);
            //    case "1221":
            //        return _Mortgage.OrderByDescending(x => x.MortgageType).ThenBy(x => x.InterestRate);
            //    case "1222":
            //        return _Mortgage.OrderByDescending(x => x.MortgageType).ThenByDescending(x => x.InterestRate);
            //    default:
            //        return _Mortgage.OrderBy(x => x.MortgageType);
            //}
        }
    }
    public class MortageCalculator
    {
        public double Principle { get; set; }
        public int TotalYears { get; set; }
        public double Premium { get; set; }
        public double IntrestRate { get; set; }
        Dictionary<string, decimal> ditPrinciple = new Dictionary<string, decimal>();
        public MortageCalculator(double Principle, int TotalYears, double Premium, double IntrestRate)
        {
            this.Principle = Principle;
            this.TotalYears = TotalYears;
            this.IntrestRate = IntrestRate;
            this.Premium = Premium == 0 ? calculatePremium() : Premium;
        }
        public Result Calculate()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddYears(TotalYears);
            Result _Result = new Result();
            _Result.YResult = Enumerable.Range(0, TotalYears * 12).Select(res => new
            {
                Date = DateTime.Now.AddMonths(res),
                InterestPaid = GetInterest(),
                PrincipalPaid = GetPrincipalPaid(),
                OutstandingLoanBalance = OutstandingLoanBalance()
            }).TakeWhile(w => w.PrincipalPaid > 0).ToArray().GroupBy(grp => grp.Date.Year).Select(grpres => new YResult()
            {
                Year = grpres.Key,
                PrincipalPaid = grpres.Sum(sm => sm.PrincipalPaid),
                InterestPaid = grpres.Sum(sm => sm.InterestPaid),
                OutstandingLoanBalance = grpres.LastOrDefault().OutstandingLoanBalance,
                MResult = grpres.Select(rec => new MResult()
                {
                    CurrentMonth = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(rec.Date.Month),
                    InterestPaid = rec.InterestPaid,
                    PrincipalPaid = rec.PrincipalPaid,
                    OutstandingLoanBalance = rec.OutstandingLoanBalance
                }).ToArray()

            }).ToArray();
            _Result.TotalIntrestPaid = _Result.YResult.Sum(interest => interest.InterestPaid);
            _Result.TotalRepayment = _Result.TotalIntrestPaid + _Result.YResult.Sum(Principal => Principal.PrincipalPaid);
            return _Result;
        }
        private bool isLoanCompleted()
        {
            if (this.Principle <= 0)
            {
                this.Principle = 0;
                this.Premium = 0;
                this.IntrestRate = 0;
                return true;
            }
            return false;
        }
        private double calculatePremium()
        {
            var loanAmount = this.Principle;
            var rateOfInterest = this.IntrestRate / 1200;
            var numberOfPayments = this.TotalYears * 12;
            var paymentAmount = (rateOfInterest * loanAmount) / (1 - Math.Pow(1 + rateOfInterest, numberOfPayments * -1));
            return Math.Round(paymentAmount, 2);
        }
        private double GetInterest()
        {
            return isLoanCompleted() ? 0 : Math.Round((this.Principle * this.IntrestRate) / 1200, 2);
        }
        private double GetPrincipalPaid()
        {
            if (isLoanCompleted())
            {
                return 0;
            }
            if ((this.Premium - GetInterest()) > this.Principle)
            {
                this.Premium -= (this.Premium - GetInterest()) - this.Principle;
                return this.Premium;
            }
            return Math.Round(this.Premium - GetInterest(), 2);
        }
        private double OutstandingLoanBalance()
        {
            if (this.Principle <= 0)
            {
                this.Principle = 0;
                this.Premium = 0;
                this.IntrestRate = 0;
                return 0;
            }
            this.Principle -= GetPrincipalPaid();
            this.Principle = Math.Round(this.Principle, 2);
            return this.Principle;
        }

    }



}