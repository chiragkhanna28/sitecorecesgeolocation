using CES.GeoIP.Models;
using Sitecore.CES.Client;
using Sitecore.CES.Discovery;
using Sitecore.CES.GeoIp;
using Sitecore.CES.GeoIp.Configuration;
using Sitecore.CES.GeoIp.Core.Model;
using Sitecore.CES.GeoIp.Pipelines.HandleLookupError;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using System;

namespace CES.GeoIP.Provider
{
    public class CustomSitecoreProvider : CustomSitecoreProviderBase
    {
        protected readonly ResourceConnector<CustomWhoIsInformation> GeoIpConnector;
        public CustomSitecoreProvider(ResourceConnector<CustomWhoIsInformation> geoIpConnector)
        {
            GeoIpConnector = geoIpConnector;
        }
        public override CustomWhoIsInformation GetWhoIsInformationByIp(string ip)
        {
            Assert.ArgumentNotNullOrEmpty(ip, nameof(ip));
            try
            {
                CustomWhoIsInformation isInformationByIp = this.RequestGeoIpService(ip);
                return (CustomWhoIsInformation)isInformationByIp;
            }
            catch (Exception ex)
            {
                HandleLookupErrorArgs args = new HandleLookupErrorArgs(ex);
                CorePipeline.Run("ces.geoIp.handleLookupError", (PipelineArgs)args);
                if (args.Fallback != null)
                    return (CustomWhoIsInformation)args.Fallback;
                throw;
            }
        }

        protected CustomWhoIsInformation RequestGeoIpService(string ip)
        {
            Assert.ArgumentNotNullOrEmpty(ip, nameof(ip));
            var endPointSource = DiscoveryDefaults.Instance.GetEndpointSource();
            string endpoint = endPointSource.GetEndpoint(Settings.GeoIpServiceName);
            Assert.IsNotNullOrEmpty(endpoint, "geoIpServiceUri");
            return this.GeoIpConnector.Request(endpoint, (object)ip);
        }
    }
}
