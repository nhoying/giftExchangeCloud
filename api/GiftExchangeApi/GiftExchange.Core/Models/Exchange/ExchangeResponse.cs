namespace GiftExchange.Core.Models.Exchange;

public class ExchangeResponse
{
    public Guid ExchangeIdentifier { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int MaxSwipes { get; set; }
    public int MaxTurnActions { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
}