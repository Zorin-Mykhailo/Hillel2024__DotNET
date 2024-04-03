using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Store.Contract.ProductInOrder.Requests;
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
    public async Task<IActionResult> InsertOrderAsync([FromServices] IRequestHandler<InsertOrderRequest, OrderResponse> insertOrderCommand, [FromBody] InsertOrderRequest request)
    {
        OrderResponse order = await insertOrderCommand.Handle(request);
        return Ok(order);
    }


    [HttpPut("{orderId}")]
    public async Task<IActionResult> UpdateOrderAsync(int orderId, [FromServices] IRequestHandler<int, UpdateOrderRequest, OrderResponse?> updateOrderCommand, [FromBody] UpdateOrderRequest request)
    {
        OrderResponse? order = await updateOrderCommand.Handle(orderId, request);
        return order != null ? Ok(order) : NotFound();
    }


    [HttpPost("{orderId}/OrderLine")]
    public async Task<IActionResult> UpsertOrderLineAsync(int orderId, [FromServices] IRequestHandler<int, UpsertOrderLineCommand, OrderLineResponse> upsertOrderLineComand, [FromBody] UpsertOrderLineRequest request)
    {
        OrderLineResponse orderLineResponse = await upsertOrderLineComand.Handle(orderId, new UpsertOrderLineCommand
        {
            ProductId = request.ProductId,
            Notes = request.Notes,
            ProductAmount = request.ProductAmount,
        });
        return Ok(orderLineResponse);
    }


    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync([FromServices] IRequestHandler<IList<OrderResponse>> getOrdersQuery)
        => Ok(await getOrdersQuery.Handle());



    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderByIdAsync(int orderId, [FromServices] IRequestHandler<int, OrderResponse?> getOrderByIdQuery)
        => Ok(await getOrderByIdQuery.Handle(orderId));



    [HttpGet("{orderId}/OrderLine")]
    public async Task<IActionResult> GetOrderLinesOfOrder(int orderId, [FromServices] IRequestHandler<int, IList<OrderLineResponse>> getOrderLinesByProductIdQuery)
        => Ok(await getOrderLinesByProductIdQuery.Handle(orderId));



    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrderById(int orderId, [FromServices] IRequestHandler<DeleteOrderRequest, bool> deleteOrderById)
        => await deleteOrderById.Handle(new DeleteOrderRequest(orderId)) ? Ok(true) : NotFound();



    [HttpDelete("{orderId}/OrderLine")]
    public async Task<IActionResult> DeleteOrderLinesOfOrder(int orderId, [FromServices] IRequestHandler<DeleteOrderLinesCommand, bool> deleteOrderLines)
        => await deleteOrderLines.Handle(new DeleteOrderLinesCommand(orderId)) ? Ok(true) : NotFound();



    [HttpDelete("{orderId}/OrderLine/{productId}")]
    public async Task<IActionResult> DeleteOrderLineOfOrderById(int orderId, int productId, [FromServices] IRequestHandler<DeleteOrderLineCommand, bool> deleteOrderLine)
        => await deleteOrderLine.Handle(new DeleteOrderLineCommand(orderId, productId)) ? Ok(true) : NotFound();
}