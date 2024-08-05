using GiftExchange.Data.GiftExchangeDb.Schema;

namespace GiftExchange.Data.GiftExchangeDb;

public interface IGiftExchangeRepository
{
    // Exchange
    Task<Exchange> GetExchange(Guid exchangeIdentifier);
    Task AddExchange(Exchange exchange);
    Task UpdateExchange(Exchange exchange);
    Task RemoveExchange(Guid exchangeIdentifier);
}

public class GiftExchangeRepository
{
    
}