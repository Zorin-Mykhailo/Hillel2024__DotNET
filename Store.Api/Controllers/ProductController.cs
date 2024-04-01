using Microsoft.AspNetCore.Mvc;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Service;
using Store.Service.Commands;
using Store.Service.Queries;

namespace Store.Api.Controllers;

[Route("Controller")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync([FromServices] IRequestHandler<IList<ProductResponse>> getProductsQuery)
        => Ok(await getProductsQuery.Handle());



    //[HttpGet("{ProductId}")]
    //public async Task<IActionResult> GetProductByIdAsync(int productId, [FromServices] IRequestHandler<int, ProductResponse?> getProductByIdQuery)
    //    => Ok(await getProductByIdQuery.Handle(productId));


    //[HttpPost]
    //public async Task<IActionResult> UpsertProductAsync([FromServices] IRequestHandler<UpsertProductCommand, ProductResponse> upsertProductComand, [FromBody] UPsertProductRequest request)
    //{
    //    throw new NotImplementedException();
    //}



    //[HttpDelete("{ProductId}")]
    //public async Task<IActionResult> DeleteProductById(int productId, [FromServices] IRequestHandler<DeleteProductCommand, bool> deleteProductByIdCommand)
    //{
    //    throw new NotImplementedException();
    //}
}
