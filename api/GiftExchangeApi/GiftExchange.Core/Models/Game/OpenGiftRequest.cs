namespace GiftExchange.Core.Models.Game;

public class OpenGiftRequest
{
    public Guid ExchangeIdentifier { get; set; }
    public Guid PlayerIdentifier { get; set; }
    public string GiftDescription { get; set; }
    public bool HasLotteryTickets { get; set; }
}