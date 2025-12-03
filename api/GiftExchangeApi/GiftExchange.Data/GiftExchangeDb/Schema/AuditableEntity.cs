namespace GiftExchange.Data.GiftExchangeDb.Schema;

public abstract class AuditableEntity
{
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public DateTime ModifiedDate { get; set; }
}