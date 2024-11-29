using Microsoft.EntityFrameworkCore.Storage;

namespace ProyectoEvoltis.Infrastructure.Persistence.Interfaces
{
  public interface IUnitOfWork: IDisposable
  {
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    bool HasActiveTransaction { get; }
    IDbContextTransaction? GetCurrentTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(IDbContextTransaction transaction);
  }
}
