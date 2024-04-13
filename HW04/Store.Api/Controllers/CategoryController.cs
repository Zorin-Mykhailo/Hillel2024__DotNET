using Microsoft.AspNetCore.Mvc;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Service;
using Store.Service.Commands;
using Store.Service.Queries;

namespace Store.Api.Controllers;

[Route("[Controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UpsertCategoryAsync([FromServices] IRequestHandler<UpsertCategoryCommand, CategoryResponse> upsertCategoryComand, [FromBody] UpsertCategoryRequest request)
    {
        CategoryResponse category = await upsertCategoryComand.Handle(new UpsertCategoryCommand
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
        });

        string baseUri = $"{Request.Scheme}://{Request.Host}:{(Request.Host.Port == null ? string.Empty : $":{Request.Host.Port}")}";

        return Created( $"{baseUri}/Category/{category.Id}", category);

        //return Ok(category);
    }



    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync([FromServices] IRequestHandler<IList<CategoryResponse>> getCategoriesQuery)
        => Ok(await getCategoriesQuery.Handle());



    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryByIdAsync(int categoryId, [FromServices] IRequestHandler<int, CategoryResponse?> getCategoryByIdQuery)
        => Ok(await getCategoryByIdQuery.Handle(categoryId));

    
    [HttpGet("{categoryId}/Product")]
    public async Task<IActionResult> GetCategoryByIdWithProductsAsync(int categoryId, [FromServices] IRequestHandler<int, CategoryWithProductsResponse?> getCategoryByIdQuery)
        => Ok(await getCategoryByIdQuery.Handle(categoryId));



    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategoryById(int categoryId, [FromServices] IRequestHandler<DeleteCategoryCommand, bool> deleteCategoryByIdCommand)
        => await deleteCategoryByIdCommand.Handle(new DeleteCategoryCommand(categoryId)) ? Ok(true) : NotFound();
}
