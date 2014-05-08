using System;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    public class FinOrgFund
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public double? Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public string FundDate
        {
            get
            {

                try
                {
                    return new DateTime(this.Year, this.Month, this.Day).ToShortDateString();
                }
                catch (Exception)
                {
                    return string.Format("{0}/{1}", this.Month, this.Year);
                }

            }
        }   
    }
}