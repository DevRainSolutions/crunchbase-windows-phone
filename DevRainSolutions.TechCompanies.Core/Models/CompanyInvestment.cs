using System;

namespace DevRainSolutions.StartupsCentral.Core.Models
{
    public class CompanyInvestment
    {
        public string RoundCode { get; set; }
        public string Description { get; set; }
        public string SourceUrl { get; set; }
        public string CurrencyCode { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public double? Amount { get; set; }

        public string Name { get; set; }
        public string Permalink { get; set; }

        public string InvestmentDate
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
