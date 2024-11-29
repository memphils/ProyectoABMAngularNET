
using ProyectoEvoltis.Infrastructure.Persistence.Interfaces;

namespace ProyectoEvoltis.Infrastructure.Persistence.Pagination
{
  public static class OrderByExpression
  {
    public static IQueryable<T> Build<T>(IQueryable<T> query, ICollection<IOrderExpression<T>> orderByExpressions) where T : class
    {
      if (orderByExpressions == null)
      {
        return query;
      }

      IOrderedQueryable<T>? output = null;

      foreach (var orderByExpression in orderByExpressions)
      {
        output = output == null ? orderByExpression.ApplyOrderBy(query) : orderByExpression.ApplyThenBy(output);
      }

      return output ?? query;
    }
  }
}
