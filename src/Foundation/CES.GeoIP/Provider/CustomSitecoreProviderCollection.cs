using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.GeoIP.Provider
{
    public class CustomSitecoreProviderCollection : ProviderCollection
    {
        public CustomSitecoreProviderBase this[string name]
        {
            get
            {
                Assert.ArgumentNotNull((object)name, nameof(name));
                return base[name] as CustomSitecoreProviderBase;
            }
        }
    }
}
