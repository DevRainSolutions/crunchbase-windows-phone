using DevRainSolutions.TechCompanies.Resources;

namespace DevRainSolutions.TechCompanies.ViewModel
{
    public class LocalizedStrings
    {
        private static readonly AppResources localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return localizedResources; } }
    }
}
