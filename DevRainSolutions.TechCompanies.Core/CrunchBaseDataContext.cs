using System.Data.Linq;

namespace DevRainSolutions.TechCompanies.Core
{
    public class CrunchBaseDataContext : DataContext
    {
        public static string DbConnectionString = "Data Source=isostore:/crunchbase.sdf";

        public CrunchBaseDataContext() : base(DbConnectionString)
        {
        }

        public Table<CrunchBaseSearchItem> Companies
        {
            get
            {
                return this.GetTable<CrunchBaseSearchItem>();
            }
        }
    }
}