using CES.GeoIP.Models;
using System.Configuration.Provider;

namespace CES.GeoIP.Provider
{
    public abstract class CustomSitecoreProviderBase : ProviderBase
    {
        public abstract CustomWhoIsInformation GetWhoIsInformationByIp(string ip);
    }
}
