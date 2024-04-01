using Microsoft.AspNetCore.Mvc;
using Store.Contract.Product.Requests;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Service;
using Store.Service.Commands;
using Store.Service.Queries;

namespace Store.Api.Controllers;

[Route("[Controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UpsertProductAsync([FromServices] IRequestHandler<UpsertProductComand, ProductResponse> upsertProductComand, [FromBody] UpsertProductRequest request)
    {
        var product = await upsertProductComand.Handle(new UpsertProductComand
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            CurrentPricePerUnit = request.CurrentPricePerUnit,
        });

        return Ok(product);
    }



    [HttpGet]
    public async Task<IActionResult> GetProductsAsync([FromServices] IRequestHandler<IList<ProductResponse>> getProductsQuery)
        => Ok(await getProductsQuery.Handle());



    [HttpGet("{ProductId}")]
    public async Task<IActionResult> GetProductByIdAsync(int productId, [FromServices] IRequestHandler<int, ProductResponse?> getProductByIdQuery)
        => Ok(await getProductByIdQuery.Handle(productId));



    [HttpDelete("{ProductId}")]
    public async Task<IActionResult> DeleteProductById(int productId, [FromServices] IRequestHandler<DeleteProductCommand, bool> deleteProductByIdCommand)
        => await deleteProductByIdCommand.Handle(new DeleteProductCommand { ProductId = productId }) ? Ok(true) : NotFound();
}
