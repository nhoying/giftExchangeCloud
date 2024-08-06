using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GiftExchange.Data.GiftExchangeDb;

public interface IGiftExchangeRepository
{
    IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class;
    void Add<T>(T entity);
    void Remove<T>(T entity) where T : class;
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
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

    public IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        return GetDbSetFor<T>().Where(predicate);
    }

    public void Remove<T>(T entity) where T : class
    {
        GetDbSetFor<T>().Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    private DbSet<T> GetDbSetFor<T>() where T : class
    {
        return _context.Set<T>();
    }
}