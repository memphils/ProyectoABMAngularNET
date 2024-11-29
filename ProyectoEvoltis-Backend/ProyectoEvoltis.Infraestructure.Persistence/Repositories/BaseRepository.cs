using Microsoft.EntityFrameworkCore;
using ProyectoEvoltis.Infrastructure.Persistence.Contexts;
using ProyectoEvoltis.Infrastructure.Persistence.Pagination;
using ProyectoEvoltis.Infrastructure.Persistence.Interfaces;
using System.Linq.Expressions;

namespace ProyectoEvoltis.Infrastructure.Repositories
{
  public class BaseRepository<T> : IRepository<T> where T : class
  {
    private readonly ApplicationDbContext _context;
    protected readonly Dictionary<string, string> _exceptionData;

    public IUnitOfWork UnitOfWork
    {
      get
      {
        return _context;
      }
    }

    public BaseRepository(ApplicationDbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _exceptionData = new() { { "ContextId:", context.ContextId.ToString() }, { "ConnectionString:", context.Database.GetConnectionString() } };
    }

    public virtual IQueryable<T> GetAll(bool asNoTracking = true)
    {
      try
      {
        if (asNoTracking)
          return _context.Set<T>().AsNoTracking();
        else
          return _context.Set<T>().AsQueryable();
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual IQueryable<T> GetAllBySpec(Expression<Func<T, bool>> predicate, bool asNoTracking = true)
    {
      try
      {
        if (asNoTracking)
          return _context.Set<T>().Where(predicate).AsNoTracking();
        else
          return _context.Set<T>().Where(predicate).AsQueryable();
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
      try
      {
        _context.ChangeTracker.Clear();
        return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async Task<T?> GetBySpecAsync<Spec>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
      try
      {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async Task<T?> GetBySpecAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
      try
      {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async Task<ICollection<T>> PaginatedListAsync(int pageSize, int pageIndex, Expression<Func<T, bool>>? predicate, ICollection<IOrderExpression<T>> orderExpressionList, CancellationToken cancellationToken = default)
    {
      try
      {
        var expression = _context.Set<T>().Where(predicate ?? (x => true));
        expression = OrderByExpression.Build<T>(expression, orderExpressionList);
        return await expression
  .Skip((pageIndex - 1) * pageSize)
  .Take(pageSize)
  .ToListAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        throw;
      }

    }

    public virtual async Task<ICollection<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
      try
      {
        return await _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async Task<ICollection<T>> ListAsync(CancellationToken cancellationToken = default)
    {
      try
      {
        return await _context.Set<T>().ToListAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
      try
      {
        return await _context.Set<T>().CountAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
      try
      {
        return await _context.Set<T>().Where(predicate).CountAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
      try
      {
        return await _context.Set<T>().AnyAsync(predicate, cancellationToken);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
      try
      {
        return await _context.Set<T>().AnyAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
    {
      try
      {
        IQueryable<T> queryable = GetAll();
        foreach (Expression<Func<T, object>> includeProperty in includeProperties)
        {
          queryable = queryable.Include(includeProperty);
        }
        return queryable;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual void Add(T entity)
    {
      try
      {
        _context.Set<T>().Add(entity);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async void AddAsync(T entity, CancellationToken cancellationToken = default)
    {
      try
      {
        _context.Set<T>().Add(entity);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual void AddRange(ICollection<T> entities)
    {
      try
      {
        _context.Set<T>().AddRange(entities);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async void AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
      try
      {
        await _context.Set<T>().AddRangeAsync(entities);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual void Delete(T entity)
    {
      try
      {
        _context.Set<T>().Remove(entity);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async void DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
      try
      {
        _context.Set<T>().Remove(entity);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual void DeleteRange(ICollection<T> entities)
    {
      try
      {
        _context.Set<T>().RemoveRange(entities);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async void DeleteRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
      try
      {
        _context.Set<T>().RemoveRange(entities);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual void Update(T entity)
    {
      try
      {
        _context.Set<T>().Update(entity);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual async void UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
      try
      {
        _context.ChangeTracker.Clear();
        _context.Set<T>().Update(entity);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual void UpdateRange(ICollection<T> entities)
    {
      try
      {
        _context.Set<T>().UpdateRange(entities);
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
