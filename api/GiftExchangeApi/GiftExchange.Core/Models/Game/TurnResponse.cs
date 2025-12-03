using System.Text.Json.Serialization;
using GiftExchange.Core.Enums;
using GiftExchange.Core.Models.Player;

namespace GiftExchange.Core.Models.Game;

public class TurnResponse
{
    public Guid TurnIdentifier { get; set; }
    public int TurnNumber { get; set; }
    public PlayerResponse InitiatingPlayer { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TurnStatusEnum TurnStatus { get; set; }
}