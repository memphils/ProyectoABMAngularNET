using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProyectoEvoltis.Infrastructure.Persistence.Contexts;
using ProyectoEvoltis.Infrastructure.Persistence.Repositories;

namespace ProyectoEvoltis.Infrastructure.Persistence
{
  public static class ConfigureServices
  {
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<ApplicationDbContext>((serviceProvider, options) => {
        options.UseMySql(configuration.GetConnectionString("NorthwindConnection"), new MySqlServerVersion(new Version(8, 0, 21)),
        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
      },ServiceLifetime.Transient);
      services.AddScoped<ICustomerRepository, CustomersRepository>();

      return services;
    }
  }
}
