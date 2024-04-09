using Microsoft.AspNetCore.Mvc;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Data.Entities;
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
    public async Task<IActionResult> InsertOrderLineAsync(int orderId, [FromServices] IRequestHandler<int, InsertOrderLineRequest, OrderLineResponse> insertOrderLineComand, [FromBody] InsertOrderLineRequest request)
    {
        OrderLineResponse orderLineResponse = await insertOrderLineComand.Handle(orderId, request);
        return Ok(orderLineResponse);
    }


    [HttpPut("{orderId}/OrderLine/{productId}")]
    public async Task<IActionResult> UpdateOrderLineAsync(int orderId, int productId, [FromServices] IRequestHandler<(int orderId, int productId), UpdateOrderLineRequest, OrderLineResponse> updateOrderLineComand, [FromBody] UpdateOrderLineRequest request)
    {
        OrderLineResponse orderLineResponse = await updateOrderLineComand.Handle((orderId, productId), request);
        return orderLineResponse != null ? Ok(orderLineResponse) : NotFound();
    }


    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync([FromServices] IRequestHandler<IList<OrderResponse>> getOrdersQuery)
        => Ok(await getOrdersQuery.Handle());



    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderByIdAsync(int orderId, [FromServices] IRequestHandler<int, OrderResponse?> getOrderByIdQuery)
    {
        OrderResponse? oderResponse = await getOrderByIdQuery.Handle(orderId);
        return oderResponse != null ? Ok(oderResponse) : NotFound();
    }



    [HttpGet("{orderId}/OrderLine")]
    public async Task<IActionResult> GetOrderLinesOfOrder(int orderId, [FromServices] IRequestHandler<int, IList<OrderLineResponse>> getOrderLinesByProductIdQuery)
        => Ok(await getOrderLinesByProductIdQuery.Handle(orderId));

    [HttpGet("{orderId}/OrderLine/{productId}")]
    public async Task<IActionResult> GetOrderLineOfOrder(int orderId, int productId, [FromServices] IRequestHandler<(int orderId, int productId), OrderLineResponse?> getOrderLineByProductIdQuery)
    {
        OrderLineResponse? orderLineResponse = await getOrderLineByProductIdQuery.Handle((orderId, productId));
        return orderLineResponse != null ? Ok(orderLineResponse) : NotFound();
    }



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