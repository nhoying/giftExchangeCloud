using GiftExchange.Data.GiftExchangeDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GiftExchange.Data.DependencyInjection;

public static class DataDependencyInjectionExtensions
{
    public static IServiceCollection AddDataDependencyInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IGiftExchangeRepository, GiftExchangeRepository>();
        serviceCollection.AddDbContext<GiftExchangeContext>(builder =>
        {
            builder.UseSqlServer()
        });
        return serviceCollection;
    }
}