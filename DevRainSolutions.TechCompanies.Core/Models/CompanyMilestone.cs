using System;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    public class CompanyMilestone
    {
        public string Description { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Url { get; set; }
        public string SourceText { get; set; }
        public string SourceDescription { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string Acquirer { get; set; }

        public string Name { get; set; }
        public string Permalink { get; set; }

        public string MilestoneDate
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
