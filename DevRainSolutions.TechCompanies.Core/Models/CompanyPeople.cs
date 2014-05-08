using System.Data.Linq.Mapping;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    [Table]
    public class CompanyPeople
    {
        public bool IsPast { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Permalink { get; set; }
        public string FullName 
        { 
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
    }
}
