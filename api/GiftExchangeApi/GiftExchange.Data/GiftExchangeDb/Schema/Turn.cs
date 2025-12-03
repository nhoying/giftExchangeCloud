using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftExchange.Data.GiftExchangeDb.Schema;

public class Turn : AuditableEntity
{
    public int TurnId { get; set; }

    public int ExchangeId { get; set; }

    public int TurnNumber { get; set; }
    public Guid TurnIdentifier { get; set; }

    public int StartingPlayerId { get; set; }

    public int TurnStatusId { get; set; }

    public TurnStatus TurnStatus { get; set; }
    
    public ICollection<TurnAction> TurnActions { get; set; }

    public Player StartingPlayer { get; set; }
    
    public Exchange Exchange { get; set; }

    
    internal class ExchangeConfiguration : IEntityTypeConfiguration<Turn>
    {
        public void Configure(EntityTypeBuilder<Turn> builder)
        {
            builder.ToTable("Turns", "GiftExchange");
            builder.HasKey(a => a.TurnId);
            builder.Property(t => t.TurnId).ValueGeneratedOnAdd();
            builder.Property(t => t.TurnIdentifier).HasDefaultValueSql("NEWID()");

            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.ModifiedBy).HasMaxLength(100);

            builder.HasMany(t => t.TurnActions).WithOne(t => t.Turn)
                .HasForeignKey(t => t.TurnId);

            builder.HasOne(t => t.StartingPlayer).WithMany()
                .HasForeignKey(t => t.StartingPlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.TurnStatus)
                .WithMany()
                .HasForeignKey(a => a.TurnStatusId);
        }
    }

}