using System.Data.Linq.Mapping;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    [Table]
    public class CompanyOffice
    {
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string StateCcode { get; set; }
        public string CountryCcode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FullAdress { get; set; }
        public int Number { get; set; }


    }
}
