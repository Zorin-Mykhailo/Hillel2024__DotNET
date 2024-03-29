using Microsoft.EntityFrameworkCore;
using Store.Contract.Response.Category;
using Store.Data.Context;

namespace Store.Service.CommandsAndQueries.CategoryQueries;
public class GetCategoriesQueryHandler : IRequestHandler<int, IList<CategoryResponse>>
{
    private readonly AppDbContext _appDbContext;

    public GetCategoriesQueryHandler(AppDbContext appDbContext) => _appDbContext = appDbContext;

    public async Task<IList<CategoryResponse>> Handle(int categoryId, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Categories
            .AsNoTracking()
            .Select(v => new CategoryResponse
            {
                CreatedDate = v.CreatedDate,
                Description = v.Description,
                Id = v.Id,
                Name = v.Name,
                ProductsId = v.Products == null ? new List<int>() : v.Products.Select(e => e.ProductId).ToList(),
            }).OrderByDescending(e => e.Id)
            .ToListAsync(cancellationToken);
    }
}
