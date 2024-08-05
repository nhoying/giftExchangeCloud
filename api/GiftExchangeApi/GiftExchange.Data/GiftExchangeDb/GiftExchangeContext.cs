using Microsoft.EntityFrameworkCore;

namespace GiftExchange.Data.GiftExchangeDb;

public class GiftExchangeContext : DbContext
{
    private DbContext Context => this as DbContext;

    public GiftExchangeContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public GiftExchangeContext(DbContextOptions<GiftExchangeContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GiftExchangeContext).Assembly);
    }
}