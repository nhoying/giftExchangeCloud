using GiftExchange.Data.GiftExchangeDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GiftExchange.Data;

public class DbContextFactory : IDesignTimeDbContextFactory<GiftExchangeContext>
{
    public GiftExchangeContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
        var optionsBuilder = new DbContextOptionsBuilder<GiftExchangeContext>();
        var connectionString = configuration["CONNECTION_STRING"];
        
        Console.WriteLine($"Connection String: {connectionString}");
        optionsBuilder.UseSqlServer(connectionString);
        return new GiftExchangeContext(optionsBuilder.Options);
    }
}