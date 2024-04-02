using Microsoft.AspNetCore.Mvc;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Service;
using Store.Service.Commands;
using Store.Service.Queries;

namespace Store.Api.Controllers;

[Route("[Controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UpsertOrderAsync([FromServices] IRequestHandler<UpsertOrderCommand, OrderResponse> upsertOrderCommand, [FromBody] UpsertOrderCommand request)
    {
        OrderResponse order = await upsertOrderCommand.Handle(new UpsertOrderCommand
        {
            Id = request.Id,
            CustomerId = request.CustomerId,
            Notes = request.Notes,
        });

        return Ok(order);
    }



    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync([FromServices] IRequestHandler<IList<OrderResponse>> getOrdersQuery)
        => Ok(await getOrdersQuery.Handle());



    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderByIdAsync(int orderId, [FromServices] IRequestHandler<int, OrderResponse?> getOrderByIdQuery)
        => Ok(await getOrderByIdQuery.Handle(orderId));



    [HttpDelete("{orderId}")]
    public async Task<IActionResult> deleteOrderById(int orderId, [FromServices] IRequestHandler<DeleteOrderCommand, bool> deleteOrderByIdCommand)
        => await deleteOrderByIdCommand.Handle(new DeleteOrderCommand(orderId)) ? Ok(true) : NotFound();
}