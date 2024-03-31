using Microsoft.EntityFrameworkCore;
using Store.Contract.Response.Category;
using Store.Data.Context;

namespace Store.Service.CommandsAndQueries.CategoryQueries;
internal class GetCategoryByIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<int, CategoryResponse?>
{
    public async Task<CategoryResponse?> Handle(int categoryId, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Categories
            .AsNoTracking()
            .Where(e => e.Id == categoryId)
            .Select(v => new CategoryResponse
            {
                CreatedDate = v.CreatedDate,
                Description = v.Description,
                Id = v.Id,
                Name = v.Name,
                ProductsId = v.Products == null ? new List<int>() : v.Products.Select(e => e.ProductId).ToList(),
            }).SingleOrDefaultAsync(cancellationToken);
    }
}