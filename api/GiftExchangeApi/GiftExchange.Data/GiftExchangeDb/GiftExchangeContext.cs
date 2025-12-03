using GiftExchange.Core.Enums;
using GiftExchange.Core.Extensions;
using GiftExchange.Data.GiftExchangeDb.Schema;
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
        ApplySeedData(modelBuilder);
    }

    private void ApplySeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TurnStatus>().HasData(
            EnumExtensions.GetValues<TurnStatusEnum>().Select(a =>
                new TurnStatus
                {
                    CreatedBy = "SeedData",
                    CreatedDate = new DateTime(2024, 08, 14),
                    ModifiedBy = "SeedData",
                    ModifiedDate = new DateTime(2024, 8, 14),
                    TurnStatusId = (int)a,
                    Name = a.ToString()
                }));
        
        modelBuilder.Entity<TurnActionType>().HasData(
            EnumExtensions.GetValues<TurnActionTypeEnum>().Select(a =>
                new TurnActionType
                {
                    CreatedBy = "SeedData",
                    CreatedDate = new DateTime(2024, 08, 14),
                    ModifiedBy = "SeedData",
                    ModifiedDate = new DateTime(2024, 8, 14),
                    TurnActionTypeId = (int)a,
                    Name = a.ToString()
                }));
        
    }
}