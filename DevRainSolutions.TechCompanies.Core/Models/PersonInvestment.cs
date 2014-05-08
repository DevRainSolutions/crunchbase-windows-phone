using System;
using System.Data.Linq.Mapping;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    [Table]
    public class PersonInvestment
    {
        public string Code { get; set; }
        public string SourceUrl { get; set; }
        public string SourceDescription { get; set; }
        public string RaisedAmount { get; set; }
        public string RaisedCurrencyCode { get; set; }
        public int FundedYear { get; set; }
        public int FundedMonth { get; set; }
        public int FundedDay { get; set; }
        public string CompanyName { get; set; }
        public string Permalink { get; set; }

        public string FundedDate
        {
            get
            {
                try
                {
                    return new DateTime(this.FundedYear, this.FundedMonth, this.FundedDay).ToShortDateString();
                }
                catch (Exception)
                {
                    return string.Format("{0}/{1}", this.FundedMonth, this.FundedYear);
                }
            }
        }
    }
}
