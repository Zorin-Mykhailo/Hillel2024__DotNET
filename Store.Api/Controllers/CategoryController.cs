using Microsoft.AspNetCore.Mvc;
using Store.Contract.Request.Category;
using Store.Contract.Response.Category;
using Store.Service;
using Store.Service.CommandsAndQueries.CategoryCommands;
using System.Dynamic;

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

    [HttpPost]
    public async Task<IActionResult> UpsertCategoryAsync([FromServices] IRequestHandler<UpsertCategoryCommand, CategoryResponse> upsertCategoryComand, [FromBody] UpsertCategoryRequest request)
    {
        var category = await upsertCategoryComand.Handle(new UpsertCategoryCommand
        {
             Id = request.Id,
             Name = request.Name,
             Description = request.Description,
        });

        return Ok(category);
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategoryById(int categoryId, [FromServices] IRequestHandler<DeleteCategoryCommand, bool> deleteCategoryByIdCommand)
        => await deleteCategoryByIdCommand.Handle(new DeleteCategoryCommand { CategoryId = categoryId }) ? Ok(true) : NotFound();
}
