using CES.GeoIP.Models;
using Newtonsoft.Json.Linq;
using Sitecore.CES.Pipelines.ParseResponse;
using Sitecore.Diagnostics;
using System.Linq;

namespace CES.GeoIP.Pipelines
{
    public class CustomMapGeoIpResponse 
    {
        public void Process(ParseResponseArgs<JObject, CustomWhoIsInformation> args)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));
            if (args.Response == null)
                return;
            args.Result = this.Map(args.Response);
        }

        /// <summary>
        /// Maps specified <see cref="T:Newtonsoft.Json.Linq.JObject" /> object to <see cref="T:Sitecore.CES.GeoIp.Core.Model.WhoIsInformation" /> object.
        /// </summary>
        /// <param name="jsonObject"><see cref="T:Newtonsoft.Json.Linq.JObject" /> object to map.</param>
        /// <returns><see cref="T:Sitecore.CES.GeoIp.Core.Model.WhoIsInformation" /> object that represents mapping result.</returns>
        protected virtual CustomWhoIsInformation Map(JObject jsonObject)
        {
            Assert.ArgumentNotNull((object)jsonObject, nameof(jsonObject));
            return new CustomWhoIsInformation()
            {
                BusinessName = jsonObject["organization"].Value<string>(),
                City = jsonObject["city"].Value<string>(),
                Country = jsonObject["country"].HasValues ? jsonObject["country"][(object)"isoCode"].Value<string>() : (string)null,
                Dns = jsonObject["domain"].Value<string>(),
                Isp = jsonObject["isp"].Value<string>(),
                Latitude = jsonObject["latitude"].Value<double?>(),
                Longitude = jsonObject["longitude"].Value<double?>(),
                MetroCode = jsonObject["metroCode"].Value<string>(),
                PostalCode = jsonObject["postCode"].Value<string>(),
                TimeZone = jsonObject["timeZone"].Value<string>(),
                Region = jsonObject["subDivisionIsoCode"].HasValues ? jsonObject["subDivisionIsoCode"].Last<JToken>().Value<string>() : (string)null
            };
        }
    }
}
