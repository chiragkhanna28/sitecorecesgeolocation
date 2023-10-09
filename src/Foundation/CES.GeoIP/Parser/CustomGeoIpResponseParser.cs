using CES.GeoIP.Models;
using Newtonsoft.Json.Linq;
using Sitecore.CES.Client;
using Sitecore.CES.Pipelines.ParseResponse;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;

namespace CES.GeoIP.Parser
{
    public class CustomGeoIpResponseParser : ResponseParserBase<CustomWhoIsInformation>
    {
        public override CustomWhoIsInformation Parse(string responseStream)
        {
            Assert.ArgumentNotNullOrEmpty(responseStream, nameof(responseStream));
            ParseResponseArgs<JObject, CustomWhoIsInformation> args = new ParseResponseArgs<JObject, CustomWhoIsInformation>(responseStream);
            CorePipeline.Run("ces.geoIp.parseResponse", (PipelineArgs)args);
            return Assert.ResultNotNull<CustomWhoIsInformation>(args.Result, string.Format("The CES IP Geolocation service response can't be parsed. Check whether '{0}' pipeline logic is correct.", (object)"ces.geoIp.parseResponse"));
        }
    }
}
