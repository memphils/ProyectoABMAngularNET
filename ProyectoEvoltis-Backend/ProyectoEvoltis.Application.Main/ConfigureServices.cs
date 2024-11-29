using Microsoft.Extensions.DependencyInjection;
using ProyectoEvoltis.Application.Main.Customers;
using ProyectoEvoltis.Application.Validator;
using System.Reflection;

namespace ProyectoEvoltis.Application.Main
{
  public static class ConfigureServices
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddScoped<ICustomersApplication, CustomersApplication>();

      services.AddTransient<CustomersDtoValidator>();
      return services;
    }
  }
}
