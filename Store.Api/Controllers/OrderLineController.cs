using Microsoft.AspNetCore.Mvc;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Service;
using Store.Service.Commands;
using Store.Service.Queries;

namespace Store.Api.Controllers;

[Route("[Controller]")]
[ApiController]
public class OrderLineController : ControllerBase
{
    //[HttpPost]
    //public async Task<IActionResult> UsertOrderLineAsync([FromServices] IRequestHandler<UpsertOrderLineCommand, OrderLineResponse> upsertOrderLineComand, [FromBody] UpdateOrderLineRequest request)
    //{
    //    OrderLineResponse orderLine = await upsertOrderLineComand.Handle(new UpsertOrderLineCommand
    //    {
    //        ProductId = request.ProductId,
    //        Notes = request.Notes,
    //        ProductAmount = request.ProductAmount,
    //    });

    //    return Ok(orderLine);
    //}



    [HttpGet]
    public async Task<IActionResult> GetOrderLineAsync([FromServices] IRequestHandler<IList<OrderLineResponse>> getOrderLinesQuery)
        => Ok(await getOrderLinesQuery.Handle());
}
