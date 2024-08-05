using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftExchange.Data.GiftExchangeDb.Schema;

public class Player
{
    public int PlayerId { get; set; }
    public int ExchangeId { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    
    internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players", "GiftExchange");

            builder.HasKey(t => t.PlayerId);
            builder.Property(t => t.PlayerId).ValueGeneratedOnAdd();

            builder.Property(t => t.Name).HasMaxLength(100);
            builder.Property(t => t.PictureUrl).HasMaxLength(1000);
            
            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.ModifiedBy).HasMaxLength(100);
        }
    }
}