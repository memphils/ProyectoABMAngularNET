using ProyectoEvoltis.Infrastructure.Persistence.Interfaces;
using System.Linq.Expressions;

namespace ProyectoEvoltis.Infrastructure.Persistence.Pagination
{
  public class OrderExpression<T, TOrderBy> : IOrderExpression<T> where T : class
  {
    private readonly Expression<Func<T, TOrderBy>> expression;
    private readonly OrderDirection direction;


    public OrderExpression(Expression<Func<T, TOrderBy>> expression, OrderDirection direction = OrderDirection.Asc)
    {
      this.expression = expression;
      this.direction = direction;
    }

    public IOrderedQueryable<T> ApplyOrderBy(IQueryable<T> query)
    {
      if (this.direction == OrderDirection.Desc)
      {
        return query.OrderByDescending(this.expression);
      }

      return query.OrderBy(this.expression);
    }

    public IOrderedQueryable<T> ApplyThenBy(IOrderedQueryable<T> query)
    {
      if (this.direction == OrderDirection.Desc)
      {
        return query.ThenByDescending(this.expression);
      }

      return query.ThenBy(this.expression);
    }
  }
}
