using System.Data.Linq.Mapping;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    [Table]
    public class PersonFirm
    {
        public bool IsPast { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Permalink { get; set; }
        public string Type { get; set; }
    }
}
