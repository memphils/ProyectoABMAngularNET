using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProyectoEvoltis.Domain.Entity;
using ProyectoEvoltis.Infrastructure.Persistence.Interfaces;

namespace ProyectoEvoltis.Infrastructure.Persistence.Contexts
{
  public class ApplicationDbContext : DbContext, IUnitOfWork
  {
    private IDbContextTransaction? _currentTransaction;
    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    public DbSet<Customer> Customers => Set<Customer>();


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Customer>()
              .Property(s => s.CustomerId)
              .HasColumnName("CustomerID")
              .IsRequired();
      base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      var result = await base.SaveChangesAsync(cancellationToken);
      return result;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
      if (_currentTransaction != null) return _currentTransaction;

      _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);

      return _currentTransaction;
    }

    public async Task CommitAsync(IDbContextTransaction transaction)
    {
      if (transaction == null) throw new ArgumentNullException(nameof(transaction));
      if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

      try
      {
        await SaveChangesAsync();
        transaction.Commit();
      }
      catch
      {
        RollbackTransaction();
        throw;
      }
      finally
      {
        if (_currentTransaction != null)
        {
          _currentTransaction.Dispose();
          _currentTransaction = null!;
        }
      }
    }

    private void RollbackTransaction()
    {
      try
      {
        _currentTransaction?.Rollback();
      }
      finally
      {
        if (_currentTransaction != null)
        {
          _currentTransaction.Dispose();
          _currentTransaction = null!;
        }
      }
    }

    public override void Dispose()
    {
      base.Dispose();
      GC.SuppressFinalize(this);
    }

  }
}
