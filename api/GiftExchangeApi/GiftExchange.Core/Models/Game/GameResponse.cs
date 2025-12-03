using GiftExchange.Core.Models.Player;

namespace GiftExchange.Core.Models.Game;

public class GameResponse
{
    public bool IsGameOver { get; set; }
    public Guid ExchangeIdentifier { get; set; }
    public string ExchangeName { get; set; }
    public string ExchangeDescription { get; set; }
    public int CurrentTurnNumber { get; set; }
    public int TotalTurnNumber { get; set; }
    public int MaxSwipes { get; set; }
    public int MaxTurnActions { get; set; }
    public List<GiftResponse> Gifts { get; set; }
    public List<TurnResponse> Turns { get; set; }
    public PlayerResponse? ActivePlayer { get; set; }
    public PlayerResponse? OnDeckPlayer { get; set; }
}