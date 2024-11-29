using System.Linq.Expressions;
using System.Reflection;

namespace ProyectoEvoltis.Transversal.Common
{
  public static class StaticExpression
  {
    public static Expression<Func<TItem, bool>> PropertyEquals<TItem, TValue>(PropertyInfo property, TValue value)
    {
      var param = Expression.Parameter(typeof(TItem));
      var body = Expression.Equal(Expression.Property(param, property), Expression.Constant(value));
      return Expression.Lambda<Func<TItem, bool>>(body, param);
    }

    public static Expression<Func<TItem, bool>> PropertyEqualsAllowNull<TItem, TValue>(PropertyInfo property, TValue value)
    {
      var param = Expression.Parameter(typeof(TItem));
      var col = Expression.MakeMemberAccess(param, property);
      var valueConst = Expression.Constant(value, typeof(TValue));
      var body = EqualNullables(col, valueConst);
      return Expression.Lambda<Func<TItem, bool>>(body, param);
    }

    public static Expression<Func<TItem, bool>> PropertyContainsAllowNull<TItem, TValue>(PropertyInfo property, TValue[] values)
    {
      var param = Expression.Parameter(typeof(TItem));
      var m = Expression.MakeMemberAccess(param, property);
      var igualAValores = values.Select(valor => EqualNullables(m, Expression.Constant(valor)));
      Expression body = igualAValores.Aggregate(Expression.OrElse);
      return Expression.Lambda<Func<TItem, bool>>(body, param);
    }

    public static Expression<Func<TItem, bool>> PropertyContains<TItem>(PropertyInfo propertyInfo, string value)
    {
      var param = Expression.Parameter(typeof(TItem));
      var m = Expression.MakeMemberAccess(param, propertyInfo);
      var c = Expression.Constant(value, typeof(string));
      var mi = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
      var body = Expression.Call(m, mi, c);
      return Expression.Lambda<Func<TItem, bool>>(body, param);
    }

    public static Expression<Func<TItem, bool>> AndExpression<TItem>(this Expression<Func<TItem, bool>> left, Expression<Func<TItem, bool>>? right)
    {
      if (right == null)
        return left;
      ParameterExpression paramLeft = left.Parameters[0];
      ParameterExpression paramRigth = right.Parameters[0];
      if (ReferenceEquals(paramLeft, paramRigth))
        return Expression.Lambda<Func<TItem, bool>>(Expression.AndAlso(left.Body, right.Body), paramLeft);

      return Expression.Lambda<Func<TItem, bool>>(Expression.AndAlso(left.Body, Expression.Invoke(right, paramLeft)), paramLeft);
    }

    public static Expression<Func<TItem, bool>> OrExpression<TItem>(this Expression<Func<TItem, bool>> left, Expression<Func<TItem, bool>>? right)
    {
      if (right == null)
        return left;
      ParameterExpression paramLeft = left.Parameters[0];
      ParameterExpression paramRigth = right.Parameters[0];
      if (ReferenceEquals(paramLeft, paramRigth))
        return Expression.Lambda<Func<TItem, bool>>(Expression.OrElse(left.Body, right.Body), paramLeft);

      return Expression.Lambda<Func<TItem, bool>>(Expression.OrElse(left.Body, Expression.Invoke(right, paramLeft)), paramLeft);
    }

    private static Expression EqualNullables(Expression e1, Expression e2)
    {
      if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
      {
        e2 = Expression.Convert(e2, e1.Type);
      }
      else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
      {
        e1 = Expression.Convert(e1, e2.Type);
      }
      return Expression.Equal(e1, e2);
    }

    private static bool IsNullableType(Type t)
    {
      return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
  }
}
