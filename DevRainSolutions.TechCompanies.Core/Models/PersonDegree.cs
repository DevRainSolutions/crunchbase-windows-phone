using System;
using System.Data.Linq.Mapping;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    [Table]
    public class PersonDegree
    {
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Institution { get; set; }
        public int GraduatedYear { get; set; }
        public int GraduatedMonth { get; set; }
        public int GraduatedDay { get; set; }

        public string InstitutionFull
        { 
            get
            {
                return string.Format("{0}, {1}", this.Institution, this.Type);
            }
        }

        public string GraduatedDate
        {
            get
            {
                try
                {
                    return new DateTime(this.GraduatedYear, this.GraduatedMonth, this.GraduatedDay).ToShortDateString();
                }
                catch (Exception)
                {
                    return string.Format("{0}/{1}", this.GraduatedMonth, this.GraduatedYear);
                }
            }
        }
    }
}
