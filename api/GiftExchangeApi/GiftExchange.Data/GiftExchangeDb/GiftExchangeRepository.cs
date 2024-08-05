using System.Linq.Expressions;
using GiftExchange.Data.GiftExchangeDb.Schema;

namespace GiftExchange.Data.GiftExchangeDb;

public interface IGiftExchangeRepository
{
    Task<IQueryable<TEntity>> Query<TEntity>(Expression<Func<TEntity, bool>> predicate);
    void Add<T>(T entity);
    void Update<T>(T entity);
    void Remove<T>(T entity);
}

public class GiftExchangeRepository : IGiftExchangeRepository
{
    private readonly GiftExchangeContext _context;
    public GiftExchangeRepository(GiftExchangeContext context) 
    {
        _context = context;
    }

    public void Add<T>(T entity)
    {
        _context.Add(entity);
    }

    public IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> predicate)
    {
        _context.Set<TEntity>().AsQueryable().Where(predicate);
    }

    public void Remove<T>(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update<T>(T entity)
    {
        throw new NotImplementedException();
    }
}