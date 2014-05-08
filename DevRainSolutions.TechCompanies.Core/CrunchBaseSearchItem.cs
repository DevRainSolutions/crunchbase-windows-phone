using System;
using System.Data.Linq.Mapping;
using DevRainSolutions.TechCompanies.Core.Models;

namespace DevRainSolutions.TechCompanies.Core
{
    [Table]
    public class CrunchBaseSearchItem
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public Guid Id { get; set; }

        [Column(CanBeNull = true)]
        public string name { get; set; }

        [Column(CanBeNull = false)]
        public DateTime DateAdded { get; set; }

        [Column(CanBeNull = true)]
        public string first_name { get; set; }

        [Column(CanBeNull = true)]
        public string last_name { get; set; }

        [Column(CanBeNull = false)]
        public string permalink { get; set; }
        public string crunchbase_url;

        [Column(CanBeNull = false)]
        public string @namespace { get; set; }
        public string overview { get; set; }

        public Image image { get; set; }

        public CrunchbaseTypes Type
        {
            get
            {
                switch (@namespace)
                {
                    case "person": return CrunchbaseTypes.Person;
                    case "company": return CrunchbaseTypes.Company;
                    case "product": return CrunchbaseTypes.Product;
                    default: return CrunchbaseTypes.None;
                }
            }
        }

        public string imageUrl
        {
            get
            {
                if (image != null && image.available_sizes != null && image.available_sizes.Count != 0)
                {
                    return "http://crunchbase.com/"+ (string)image.available_sizes[0][1];
                }

                return null;
            }
        }

        public string Name
        {
            get
            {
                return @namespace == "person" ? string.Format("{0} {1}", first_name, last_name) : name;
            }
        }
    }
}
