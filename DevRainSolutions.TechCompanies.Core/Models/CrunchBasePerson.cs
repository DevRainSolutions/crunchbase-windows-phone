using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    [Table]
    public class CrunchBasePerson
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false)]
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }        

        public string Permalink { get; set; }
        public string Overview { get; set; }
        public string Website { get; set; }
        public string Birthplace { get; set; }
        public int BornYear { get; set; }
        public int BornMonth { get; set; }
        public int BornDay { get; set; }
        public string Blog { get; set; }
        public string BlogFeedUrl { get; set; }
        public string TwitterUserName { get; set; }
        public string AffiliationName { get; set; }
        public string CrunchBaseUrl { get; set; }
        public string Namespace { get; set; }
        public string ImageUrl { get; set; }

        public List<PersonFirm> Firms { get; set; }
        public List<PersonDegree> Degrees { get; set; }
        public List<PersonMilestone> Milestones { get; set; }
        //public List<PersonInvestment> Investments { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public string BornDate
        {
            get
            {
                try
                {
                    return new DateTime(this.BornYear, this.BornMonth, this.BornDay).ToShortDateString();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
    }
}
