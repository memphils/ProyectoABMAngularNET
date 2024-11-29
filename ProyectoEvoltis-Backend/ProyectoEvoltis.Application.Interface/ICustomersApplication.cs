using ProyectoEvoltis.Application.DTO;
using ProyectoEvoltis.Transversal.Common;

namespace ProyectoEvoltis.Application.Interface
{
  public interface ICustomersApplication
  {
    #region Métodos Asíncronos
    Task<Response<bool>> InsertAsync(CustomerDto customersDto);
    Task<Response<bool>> UpdateAsync(CustomerDto customersDto);
    Task<Response<bool>> DeleteAsync(int customerId);

    Task<Response<CustomerDto>> GetAsync(int customerId);
    Task<Response<IEnumerable<CustomerDto>>> GetAllAsync();
    //Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
    #endregion
  }
}
