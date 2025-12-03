using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using GiftExchange.Data.GiftExchangeDb.Schema;
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
        var addedEntries = _context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added)
            .ToArray();

        foreach (var e in addedEntries)
        {
            if (e.Entity is AuditableEntity entry)
            {
                entry.CreatedBy = "TBD";
                entry.ModifiedBy = "TBD";
                entry.CreatedDate = DateTime.Now;
                entry.ModifiedDate = DateTime.Now;
            }
        }
        
        var modified = _context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified)
            .ToArray();
        
        foreach (var e in modified)
        {
            if (e.Entity is AuditableEntity entry)
            {
                entry.ModifiedBy = "TBD";
                entry.ModifiedDate = DateTime.Now;
            }
        }
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    private DbSet<T> GetDbSetFor<T>() where T : class
    {
        return _context.Set<T>();
    }
}