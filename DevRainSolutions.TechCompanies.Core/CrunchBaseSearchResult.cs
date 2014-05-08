using System.Collections.ObjectModel;

namespace DevRainSolutions.TechCompanies.Core
{
    public class CrunchBaseSearchResult
    {
        public int total { get; set; }
        public int page { get; set; }
        public string crunchbase_url { get; set; }

        public ObservableCollection<CrunchBaseSearchItem> results = new ObservableCollection<CrunchBaseSearchItem>();
    }
}