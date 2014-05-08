namespace DevRainSolutions.TechCompanies.Core.Models
{
    public class LocationInfo //Class for represent points on the map
    {
        public System.Device.Location.GeoCoordinate Location { get; set; }
        public int Number { get; set; }

        public LocationInfo()
        {
            this.Location = new System.Device.Location.GeoCoordinate();
        }
    }
}