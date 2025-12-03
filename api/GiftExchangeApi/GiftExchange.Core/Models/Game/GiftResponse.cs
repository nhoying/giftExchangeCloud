using GiftExchange.Core.Models.Player;

namespace GiftExchange.Core.Models.Game;

public class GiftResponse
{
    public Guid GiftIdentifier { get; set; }
    public string GiftName { get; set; }
    public PlayerResponse HoldingPlayer { get; set; }
    public bool HasLotteryTickets { get; set; }
    public int NumberOfTimesSwiped { get; set; }
    public bool ShowAsAvailable { get; set; }
}