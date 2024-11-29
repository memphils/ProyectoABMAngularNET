namespace ProyectoEvoltis.Infrastructure.Persistence.Interfaces
{
  public interface IOrderExpression<T> where T : class
  {
    IOrderedQueryable<T> ApplyOrderBy(IQueryable<T> query);
    IOrderedQueryable<T> ApplyThenBy(IOrderedQueryable<T> query);
  }
}
