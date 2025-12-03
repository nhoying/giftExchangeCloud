using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftExchange.Data.GiftExchangeDb.Schema;

public class TurnStatus : AuditableEntity
{
    public int TurnStatusId { get; set; }
    
    public required string Name { get; set; }
    
    internal class TurnActionTypeConfiguration : IEntityTypeConfiguration<TurnStatus>
    {
        public void Configure(EntityTypeBuilder<TurnStatus> builder)
        {
            builder.ToTable("TurnStatuses", "GiftExchange");
            builder.HasKey(t => t.TurnStatusId);
            builder.Property(t => t.Name).HasMaxLength(50);
            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.ModifiedBy).HasMaxLength(100);
        }
    }
}