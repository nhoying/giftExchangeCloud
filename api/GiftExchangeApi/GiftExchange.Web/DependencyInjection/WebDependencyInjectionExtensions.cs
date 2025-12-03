using GiftExchange.Core.Configuration;
using GiftExchange.Data.DependencyInjection;
using GiftExchange.Services.DependencyInjection;

namespace GiftExchange.Web.DependencyInjection;

public static class WebDependencyInjectionExtensions
{
    public static IServiceCollection AddWebDependencyInjection(this IServiceCollection serviceCollection)
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddEnvironmentVariables();

        var configRoot = configBuilder.Build();
        var configuration = new GiftExchangeConfiguration();
        configRoot.Bind(configuration);

        serviceCollection.AddControllers();
        serviceCollection.AddDataDependencyInjection(configuration);
        serviceCollection.AddServiceDependencyInjection();

        
        
        serviceCollection.AddSwaggerGen(options => { });
        return serviceCollection;
    }
}