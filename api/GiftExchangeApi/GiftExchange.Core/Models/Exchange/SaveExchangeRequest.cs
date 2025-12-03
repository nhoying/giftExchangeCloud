namespace GiftExchange.Core.Models.Exchange;
/// <summary>
/// Model used for updating/creating a Gift Exchange
/// </summary>
public class SaveExchangeRequest
{
    /// <summary>
    /// Identifier for the given exchange. Can be null if inserting a new one.
    /// </summary>
    public Guid? ExchangeIdentifier { get; set; }
    
    /// <summary>
    /// Name of the exchange. Required.
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Description of the exchange. Required.
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// Max Times a gift can be stolen/swiped. Required.
    /// </summary>
    public int MaxSwipes { get; set; }
    
    /// <summary>
    /// Max number of actions for a given turn. Required.
    /// </summary>
    public int MaxTurnActions { get; set; }
}