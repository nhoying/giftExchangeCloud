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

public class GiftExchangeRepository : IGiftExchangeRepository
{
    private readonly GiftExchangeContext _context;
    public GiftExchangeRepository(GiftExchangeContext context) 
    {
        _context = context;
    }

    public Task AddExchange(Exchange exchange)
    {
        throw new NotImplementedException();
    }

    public Task<Exchange> GetExchange(Guid exchangeIdentifier)
    {
        throw new NotImplementedException();
    }

    public Task RemoveExchange(Guid exchangeIdentifier)
    {
        throw new NotImplementedException();
    }

    public Task UpdateExchange(Exchange exchange)
    {
        throw new NotImplementedException();
    }
}