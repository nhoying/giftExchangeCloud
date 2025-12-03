using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftExchange.Data.GiftExchangeDb.Schema;

public class Gift : AuditableEntity
{
    public int GiftId { get; set; }

    public Guid GiftIdentifier { get; set; }
    
    public required string Description { get; set; }

    public bool HasLotteryTickets { get; set; }
        
    public int ExchangeId { get; set; }

    public required Exchange Exchange { get; set; }
    
    internal class ExchangeConfiguration : IEntityTypeConfiguration<Gift>
    {
        public void Configure(EntityTypeBuilder<Gift> builder)
        {
            builder.ToTable("Gifts", "GiftExchange");
            builder.HasKey(a => a.GiftId);
            builder.Property(t => t.GiftId).ValueGeneratedOnAdd();

            builder.Property(t => t.Description).HasMaxLength(1500);
            builder.Property(t => t.GiftIdentifier).HasDefaultValueSql("NEWID()");

            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.ModifiedBy).HasMaxLength(100);
        }
    }
}