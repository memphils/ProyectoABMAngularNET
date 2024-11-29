namespace ProyectoEvoltis.Infrastructure.Persistence.Pagination
{
  public record FilterParameter
  {
    public string Field { get; init; }

    public string Value { get; init; }
  }
}
