namespace DevRainSolutions.TechCompanies.Core
{
    public static class Constants
    {
        /// <summary>
        /// Crunchbase API key
        /// </summary>
        public static string ApiKey = "wtq8xryw8gs8jx4mxvdw4thw";

        public static string SearchQuery
        {
            get
            {
                return "http://api.crunchbase.com/v/1/search.js?query={0}&page={1}&api_key=" + ApiKey;
            }
        }

        public static string CompanyQuery
        {
            get
            {
                return "http://api.crunchbase.com/v/1/{0}/{1}.js?api_key=" + ApiKey;
            }
        }

        public const string CrunchbaseSite = "http://crunchbase.com/";

        public const double DefaultRequestTimeout = 15000;
    }
}
