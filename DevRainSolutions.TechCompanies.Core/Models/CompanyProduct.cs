using System.Data.Linq.Mapping;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    [Table]
    public class CompanyProduct
    {
        public string Name { get; set; }
        public string Permalink { get; set; }
    }
}
