namespace GiftExchange.Core.Models.Player;

public class PlayerResponse
{
    public Guid PlayerIdentifier { get; set; }
    public Guid ExchangeIdentifier { get; set; }
    public string Name { get; set; }
    public string? PictureUrl { get; set; }
}