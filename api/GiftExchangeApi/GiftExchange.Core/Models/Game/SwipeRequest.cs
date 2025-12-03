namespace GiftExchange.Core.Models.Game;

public class SwipeRequest
{
    public Guid ExchangeIdentifier { get; set; }
    public Guid GiftIdentifier { get; set; }
    public Guid SwipingPlayerIdentifier { get; set; }
}