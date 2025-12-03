using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftExchange.Data.GiftExchangeDb.Schema;

public class TurnActionType : AuditableEntity
{
    public int TurnActionTypeId { get; set; }
    public required string Name { get; set; }
    
    internal class TurnActionTypeConfiguration : IEntityTypeConfiguration<TurnActionType>
    {
        public void Configure(EntityTypeBuilder<TurnActionType> builder)
        {
            builder.ToTable("TurnActionTypes", "GiftExchange");
            builder.HasKey(t => t.TurnActionTypeId);
            builder.Property(t => t.Name).HasMaxLength(50);
            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.ModifiedBy).HasMaxLength(100);
        }
    }
}