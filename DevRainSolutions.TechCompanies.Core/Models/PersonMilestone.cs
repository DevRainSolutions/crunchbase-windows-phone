using System.Data.Linq.Mapping;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    [Table]
    public class PersonMilestone : CompanyMilestone
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
