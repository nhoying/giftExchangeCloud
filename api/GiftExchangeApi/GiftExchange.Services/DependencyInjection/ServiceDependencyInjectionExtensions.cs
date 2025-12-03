using Microsoft.Extensions.DependencyInjection;

namespace GiftExchange.Services.DependencyInjection;

public static class ServiceDependencyInjectionExtensions
{
    public static IServiceCollection AddServiceDependencyInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IExchangeManagementService, ExchangeManagementService>();

        return serviceCollection;
    }
}