using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftExchange.Data.GiftExchangeDb.Schema;

public class TurnAction
{
    public int TurnActionId { get; set; }

    public int TurnId { get; set; }
    public required Turn Turn { get; set;  }

    public int? ParentTurnActionId { get; set; }
    public TurnAction ParentTurnAction { get; set; }

    public int GiftId { get; set; }
    public Gift Gift { get; set; }

    public int PlayerId { get; set; }
    public Player Player { get; set; }

    public int TurnActionTypeId { get; set; }
    public TurnActionType TurnActionType { get; set; }

    public required string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    
    internal class TurnActionConfiguration : IEntityTypeConfiguration<TurnAction>
    {
        public void Configure(EntityTypeBuilder<TurnAction> builder)
        {
            builder.ToTable("TurnActions", "GiftExchange");
            builder.HasKey(a => a.TurnActionId);
            builder.Property(t => t.TurnActionId).ValueGeneratedOnAdd();
            
            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.ModifiedBy).HasMaxLength(100);

            builder.HasOne(t => t.Gift).WithMany().HasForeignKey(t => t.GiftId);
            builder.HasOne(t => t.Player).WithMany().HasForeignKey(t => t.PlayerId);
            builder.HasOne(t => t.TurnActionType).WithMany().HasForeignKey(t => t.TurnActionTypeId);
            builder.HasOne(t => t.ParentTurnAction).WithMany()
                .HasForeignKey(t => t.ParentTurnActionId);
        }
    }
}