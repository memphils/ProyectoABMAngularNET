namespace SmartFran.Cloud.Business.Infrastructure.Repositories.Domain.EF.Pagination
{
  public record FilterParameter
  {
    public string Field { get; init; }

    public string Value { get; init; }
  }
}
