using ProyectoEvoltis.Domain.Entity;
using ProyectoEvoltis.Infrastructure.Persistence.Contexts;
using ProyectoEvoltis.Infrastructure.Repositories;

namespace ProyectoEvoltis.Infrastructure.Persistence.Repositories
{
  public class CustomersRepository : BaseRepository<Customer>, ICustomerRepository
  {
    public readonly ApplicationDbContext _dbContext;
    public CustomersRepository(ApplicationDbContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
