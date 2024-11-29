using AutoMapper;
using ProyectoEvoltis.Application.DTO;
using ProyectoEvoltis.Application.Validator;
using ProyectoEvoltis.Domain.Entity;
using ProyectoEvoltis.Infrastructure.Persistence.Repositories;
using ProyectoEvoltis.Transversal.Common;
using System.Linq.Expressions;

namespace ProyectoEvoltis.Application.Main.Customers
{
  public class CustomersApplication : ICustomersApplication
  {
    private readonly ICustomerRepository _customersRepository;
    private readonly IMapper _mapper;
    private readonly CustomersDtoValidator _customersDtoValidator;
    private const string ERROR_TEXT = "Errores de Validación";
    public CustomersApplication(ICustomerRepository customersRepository, IMapper mapper, CustomersDtoValidator customerValidator)
    {
      _customersRepository = customersRepository ?? throw new ArgumentNullException(nameof(customersRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _customersDtoValidator = customerValidator;
    }

    #region Métodos Asíncronos
    public async Task<Response<bool>> InsertAsync(CustomerDto customersDto)
    {
      var response = new Response<bool>();
      try
      {
        var validation = await _customersDtoValidator.ValidateAsync(customersDto);
        if (!validation.IsValid)
        {
          response.Message = ERROR_TEXT;
          response.Errors = validation.Errors;
          return response;
        }
        if (customersDto is null) throw new ArgumentNullException(nameof(customersDto));
        var customer = _mapper.Map<Customer>(customersDto);
        using (var id_trans = await _customersRepository.UnitOfWork.BeginTransactionAsync())
        {
          _customersRepository.AddAsync(customer);
          await _customersRepository.UnitOfWork.CommitAsync(id_trans);
          if (id_trans!=null)
          {
            response.IsSuccess = true;
            response.Message = "Registro Exitoso!";
          }
        }
      }
      catch (Exception e)
      {
        response.Message = e.Message;
      }
      return response;
    }
    public async Task<Response<bool>> UpdateAsync(CustomerDto customersDto)
    {
      var response = new Response<bool>();
      if (customersDto is null) throw new ArgumentNullException(nameof(customersDto));
      try
      {
        var validation = await _customersDtoValidator.ValidateAsync(customersDto);
        if (!validation.IsValid)
        {
          response.Message = ERROR_TEXT;
          response.Errors = validation.Errors;
          return response;
        }
        using (var id_trans = await _customersRepository.UnitOfWork.BeginTransactionAsync())
        {
          var customer = _mapper.Map<Customer>(customersDto);
          _customersRepository.UpdateAsync(customer);
          await _customersRepository.UnitOfWork.CommitAsync(id_trans);
          if (id_trans!=null)
          {
            response.IsSuccess = true;
            response.Message = "Actualización Exitosa!";
          }
        }
      }
      catch (Exception e)
      {
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<Response<bool>> DeleteAsync(int customerId)
    {
      if (customerId == 0) throw new ArgumentNullException(nameof(customerId));
      var response = new Response<bool>();
      try
      {
        var customer = await _customersRepository.GetByIdAsync<int>(customerId);
        if (customer is null) throw new ArgumentNullException(nameof(customer));
        using (var id_trans = await _customersRepository.UnitOfWork.BeginTransactionAsync())
        {
          _customersRepository.Delete(customer);
          await _customersRepository.UnitOfWork.CommitAsync(id_trans);
          if (id_trans!=null)
          {
            response.IsSuccess = true;
            response.Message = "Eliminación Exitosa!";
          }
        }
      }
      catch (Exception e)
      {
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<Response<CustomerDto>> GetAsync(int customerId)
    {
      var response = new Response<CustomerDto>();
      try
      {
        var customer = await _customersRepository.GetByIdAsync(customerId);
        response.Data = _mapper.Map<CustomerDto>(customer);
        if (response.Data != null)
        {
          response.IsSuccess = true;
          response.Message = "Consulta Exitosa!";
        }
      }
      catch (Exception e)
      {
        response.Message = e.Message;
      }
      return response;
    }
    public async Task<Response<IEnumerable<CustomerDto>>> GetAllAsync()
    {
      var response = new Response<IEnumerable<CustomerDto>>();
      try
      {
        var customers = await _customersRepository.ListAsync();
        response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        if (response.Data != null)
        {
          response.IsSuccess = true;
          response.Message = "Consulta Exitosa!";
        }
      }
      catch (Exception e)
      {
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<Response<IEnumerable<CustomerDto>>> GetByCriteria(string filter)
    {
      var response = new Response<IEnumerable<CustomerDto>>();
      try
      {
        List<Expression<Func<Customer, bool>>> expressionList = new();
        Expression<Func<Customer, bool>>? expression = null;
        expressionList.Add(StaticExpression.PropertyContains<Customer>(typeof(Customer).GetProperty("CustomerName"), filter));

        foreach (var exp in expressionList)
        {
          expression = exp.OrExpression<Customer>(expression);
        }
        var customers = _customersRepository.GetAllBySpec(expression, false).AsEnumerable();
        response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        if (response.Data != null)
        {
          response.IsSuccess = true;
          response.Message = "Consulta Exitosa!";
        }
      }
      catch (Exception e)
      {
        response.Message = e.Message;
      }
      return response;
    }

    /*public async Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
      try
      {
        var response = new ResponsePagination<IEnumerable<CustomerDto>>();

        var count = await _customersRepository.CountAsync();

        var customers = await _customersRepository.PaginatedListAsync(pageNumber, pageSize);
        response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);

        if (response.Data != null)
        {
          response.PageNumber = pageNumber;
          response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
          response.TotalCount = count;
          response.IsSuccess = true;
          response.Message = "Consulta Paginada Exitosa!!!";
        }

        return response;
      }
      catch (Exception)
      {
        throw;
      }
    }*/
    #endregion
  }
}
