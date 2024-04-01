using Microsoft.AspNetCore.Mvc;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Service;
using Store.Service.Commands;
using Store.Service.Queries;

namespace Store.Api.Controllers;

[Route("[Controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UpsertCustomerAsync([FromServices] IRequestHandler<UpsertCustomerCommand, CustomerResponse> upsertCustomerComand, [FromBody] UpsertCustomerRequest request)
    {
        var customer = await upsertCustomerComand.Handle(new UpsertCustomerCommand
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
        });

        return Ok(customer);
    }



    [HttpGet]
    public async Task<IActionResult> GetCustomersAsync([FromServices] IRequestHandler<IList<CustomerResponse>> getCustomerQuery)
        => Ok(await getCustomerQuery.Handle());



    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerByIdAsync(int customerId, [FromServices] IRequestHandler<int, CustomerResponse?> getCustomerByIdQuery)
        => Ok(await getCustomerByIdQuery.Handle(customerId));



    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCustomerById(int customerId, [FromServices] IRequestHandler<DeleteCustomerCommand, bool> deleteCustomerByIdCommand)
        => await deleteCustomerByIdCommand.Handle(new DeleteCustomerCommand { CustomerId = customerId }) ? Ok(true) : NotFound();
}
