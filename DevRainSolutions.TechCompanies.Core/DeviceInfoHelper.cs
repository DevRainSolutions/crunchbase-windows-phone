using Microsoft.Phone.Net.NetworkInformation;

namespace DevRainSolutions.TechCompanies.Core
{
    public static class DeviceInfoHelper
    {
        public static bool NetworkExists
        {
            get
            {
                return (NetworkInterface.NetworkInterfaceType != NetworkInterfaceType.None);
            }
        }
    }
}
