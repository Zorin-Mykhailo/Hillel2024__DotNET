using Microsoft.EntityFrameworkCore;
using Store.Contract.Response.Category;
using Store.Data.Context;

namespace Store.Service.CommandsAndQueries.CategoryQueries;
internal class GetCategoryByIdQueryHandler : IRequestHandler<int, CategoryResponse?>
{
    private readonly AppDbContext _appDbContext;

    public GetCategoryByIdQueryHandler(AppDbContext appDbContext) => _appDbContext = appDbContext;

    public async Task<CategoryResponse?> Handle(int categoryId, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Categories
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