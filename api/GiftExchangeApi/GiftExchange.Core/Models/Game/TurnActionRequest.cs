using GiftExchange.Core.Enums;

namespace GiftExchange.Core.Models.Game;

public class TurnActionRequest
{
    public Guid ExchangeIdentifier { get; set; }
    public Guid TurnIdentifier { get; set; }
    public TurnActionTypeEnum TurnActionType { get; set; }
    public Guid GiftIdentifier { get; set; }
    public string GiftName { get; set; }
    public bool HasLotteryTickets { get; set; }
}