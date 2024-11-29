namespace ProyectoEvoltis.Infrastructure.Persistence.Pagination
{
  [Serializable]
  public enum OrderDirection
  {
    Asc,
    Desc
  }

  [Serializable]
  public record SortParameter
  {
    public string Field { get; init; }
    public OrderDirection Direction { get; init; }
  }
}
