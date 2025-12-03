using GiftExchange.Core.Models.Player;

namespace GiftExchange.Core.Models.Game;

public class TurnActionResponse
{
    public Guid TurnActionIdentifier { get; set; }
    public PlayerResponse ActingPlayer { get; set; }
    public PlayerResponse TargetPlayer { get; set; }
    
}