using Microsoft.AspNetCore.Mvc;
using ProyectoEvoltis.Application.DTO;
using ProyectoEvoltis.Application.Main.Customers;

namespace ProyectoEvoltis.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomersController : Controller
  {
    private readonly ICustomersApplication _customersApplication;
    public CustomersController(ICustomersApplication customersApplication)
    {
      _customersApplication = customersApplication;
    }

    #region "Métodos Asincronos"

    [HttpPost("InsertAsync")]
    public async Task<IActionResult> InsertAsync([FromBody] CustomerDto customersDto)
    {
      if (customersDto == null)
        return BadRequest();
      var response = await _customersApplication.InsertAsync(customersDto);
      if (response.IsSuccess)
        return Ok(response);

      return BadRequest(response.Message);
    }

    [HttpPut("UpdateAsync")]
    public async Task<IActionResult> UpdateAsync([FromBody] CustomerDto customersDto)
    {
      var customer = await _customersApplication.GetAsync(customersDto.CustomerId);
      if (customer.Data == null)
        return NotFound(customer.Message);

      if (customersDto == null)
        return BadRequest();
      var response = await _customersApplication.UpdateAsync(customersDto);
      if (response.IsSuccess)
        return Ok(response);

      return BadRequest(response.Message);
    }

    [HttpDelete("DeleteAsync/{customerId}")]
    public async Task<IActionResult> DeleteAsync(int customerId)
    {
      if (customerId == 0)
        return BadRequest();
      var response = await _customersApplication.DeleteAsync(customerId);
      if (response.IsSuccess)
        return Ok(response);

      return BadRequest(response.Message);
    }

    [HttpGet("GetAsync/{customerId}")]
    public async Task<IActionResult> GetAsync(int customerId)
    {
      if (customerId == 0)
        return BadRequest();
      var response = await _customersApplication.GetAsync(customerId);
      if (response.IsSuccess)
        return Ok(response);

      return BadRequest(response.Message);
    }

    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
      var response = await _customersApplication.GetAllAsync();
      if (response.IsSuccess)
        return Ok(response);

      return BadRequest(response.Message);
    }

    [HttpGet("GetByCriteriaAsync/{filter}")]
    public async Task<IActionResult> GetByCriteria(string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return BadRequest();

      var response = await _customersApplication.GetByCriteria(filter);
      if (response.IsSuccess)
        return Ok(response);

      return BadRequest(response.Message);
    }

    /* [HttpGet("GetAllWithPaginationAsync")]
     public async Task<IActionResult> GetAllWithPaginationAsync([FromQuery] int pageNumber, int pageSize)
     {
       var response = await _customersApplication.GetAllWithPaginationAsync(pageNumber, pageSize);
       if (response.IsSuccess)
         return Ok(response);

       return BadRequest(response.Message);
     }*/
    #endregion

  }
}