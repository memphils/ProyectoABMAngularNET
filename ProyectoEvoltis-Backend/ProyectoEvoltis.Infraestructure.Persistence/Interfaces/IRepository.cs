using System.Linq.Expressions;

namespace ProyectoEvoltis.Infrastructure.Persistence.Interfaces
{
  public interface IRepository<T> where T : class
  {
    IUnitOfWork UnitOfWork { get; }
    IQueryable<T> GetAll(bool asNoTracking = true);
    IQueryable<T> GetAllBySpec(Expression<Func<T, bool>> predicate, bool asNoTracking = true);
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
    Task<T?> GetBySpecAsync<Spec>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?> GetBySpecAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<ICollection<T>> PaginatedListAsync(int pageSize, int pageIndex, Expression<Func<T, bool>>? predicate, ICollection<IOrderExpression<T>> orderExpressionList, CancellationToken cancellationToken = default);
    Task<ICollection<T>> ListAsync(CancellationToken cancellationToken = default);
    Task<ICollection<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
    void Add(T entity);
    void AddAsync(T entity, CancellationToken cancellationToken = default);
    void AddRange(ICollection<T> entities);
    void AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default);
    void Delete(T entity);
    void DeleteAsync(T entity, CancellationToken cancellationToken = default);
    void DeleteRange(ICollection<T> entities);
    void DeleteRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default);
    void Update(T entity);
    void UpdateAsync(T entity, CancellationToken cancellationToken = default);
    void UpdateRange(ICollection<T> entities);
  }
}
