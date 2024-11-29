using FluentValidation;
using ProyectoEvoltis.Application.DTO;

namespace ProyectoEvoltis.Application.Validator
{
  public class CustomersDtoValidator: AbstractValidator<CustomerDto>
  {
    public CustomersDtoValidator()
    {
      RuleFor(u => u.CustomerId).NotNull();
      RuleFor(u => u.CustomerName).NotNull().NotEmpty();
      RuleFor(u => u.ContactName).NotNull().NotEmpty();
    }
  }
}
