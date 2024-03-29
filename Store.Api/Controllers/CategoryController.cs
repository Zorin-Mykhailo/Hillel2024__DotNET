using Microsoft.AspNetCore.Mvc;
using Store.Contract.Request.Category;
using Store.Contract.Response.Category;
using Store.Service;

namespace Store.Api.Controllers;

[Route("[Controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategoryAsync([FromServices] IRequestHandler<IList<CategoryResponse>> getCategoriesQuery)
        => Ok(await getCategoriesQuery.Handle());

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryByIdAsync(int categoryId, [FromServices] IRequestHandler<int, CategoryResponse> getCategoryByIdQuery)
        => Ok(await getCategoryByIdQuery.Handle(categoryId));

    public async Task<IActionResult> UpsertCategoryAsync([FromServices] IRequestHandler<UpsertCategoryRequest, CategoryResponse> upsertCategoryComand, [FromBody] UpsertCategoryRequest request)
    {
        var category = await upsertCategoryComand.Handle(new UpsertCa);
    }
}
