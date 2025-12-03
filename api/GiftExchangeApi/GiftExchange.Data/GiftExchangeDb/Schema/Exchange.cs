using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftExchange.Data.GiftExchangeDb.Schema;

public class Exchange : AuditableEntity
{
    public int ExchangeId { get; set; }
    public Guid ExchangeIdentifier { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int MaxSwipes { get; set; }
    public int MaxTurnActions { get; set; }
    
    public ICollection<Gift> Gifts { get; set; }
    public ICollection<Turn> Turns { get; set; }
    public ICollection<Player> Players { get; set; }
    
    internal class ExchangeConfiguration : IEntityTypeConfiguration<Exchange>
    {
        public void Configure(EntityTypeBuilder<Exchange> builder)
        {
            builder.ToTable("Exchanges", "GiftExchange");
            builder.HasKey(a => a.ExchangeId);
            builder.Property(t => t.ExchangeId).ValueGeneratedOnAdd();
            builder.Property(t => t.ExchangeIdentifier).HasDefaultValueSql("NEWID()");

            builder.Property(t => t.Name).HasMaxLength(500);
            builder.Property(t => t.Description).HasMaxLength(2500);

            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.ModifiedBy).HasMaxLength(100);

            builder.HasMany(t => t.Gifts).WithOne(t => t.Exchange)
                .HasForeignKey(t => t.ExchangeId);

            builder.HasMany(t => t.Players)
                .WithOne(t => t.Exchange)
                .HasForeignKey(t => t.ExchangeId);

        }
    }
}