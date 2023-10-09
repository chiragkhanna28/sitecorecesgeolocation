using CES.GeoIP.Provider;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Configuration;
using Sitecore.DependencyInjection;
using System;

namespace CES.GeoIP.Pipelines
{
    public class RegDependencies : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            ServiceCollectionServiceExtensions.AddSingleton(serviceCollection, typeof(ProviderHelper<CustomSitecoreProviderBase, CustomSitecoreProviderCollection>), (Func<IServiceProvider, object>)(x => (object)new ProviderHelper<CustomSitecoreProviderBase, CustomSitecoreProviderCollection>("customlookupManager")));
            ServiceCollectionServiceExtensions.AddSingleton(serviceCollection, typeof(CustomSitecoreProviderBase), (Func<IServiceProvider, object>)(x => (object)ServiceLocator.ServiceProvider.GetRequiredService<ProviderHelper<CustomSitecoreProviderBase, CustomSitecoreProviderCollection>>().Provider));
        }
    }
}
