using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftExchange.Data.GiftExchangeDb.Schema;

public class Turn
{
    public int TurnId { get; set; }

    public int ExchangeId { get; set; }

    public int TurnNumber { get; set; }

    public int StartingPlayerId { get; set; }

    public ICollection<TurnAction> TurnActions { get; set; }

    public required Player StartingPlayer { get; set; }
    
    public required Exchange Exchange { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }

    internal class ExchangeConfiguration : IEntityTypeConfiguration<Turn>
    {
        public void Configure(EntityTypeBuilder<Turn> builder)
        {
            builder.ToTable("Turns", "GiftExchange");
            builder.HasKey(a => a.TurnId);
            builder.Property(t => t.TurnId).ValueGeneratedOnAdd();

            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.ModifiedBy).HasMaxLength(100);

            builder.HasMany(t => t.TurnActions).WithOne(t => t.Turn)
                .HasForeignKey(t => t.TurnId);

            builder.HasOne(t => t.StartingPlayer).WithMany()
                .HasForeignKey(t => t.StartingPlayerId);
        }
    }

}