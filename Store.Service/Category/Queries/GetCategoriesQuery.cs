using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;

namespace Store.Service.CommandsAndQueries.CategoryQueries;
public class GetCategoriesQueryHandler(AppDbContext appDbContext) : IRequestHandler<IList<CategoryResponse>>
{
    public async Task<IList<CategoryResponse>> Handle(CancellationToken cancellationToken = default)
    {
        return await appDbContext.Categories
            .AsNoTracking()
            .Select(v => new CategoryResponse
            {
                CreatedDate = v.CreatedDate,
                LastModifiedDate = v.LastModifiedDate,
                Id = v.Id,
                Description = v.Description,
                Name = v.Name,
                ProductsId = v.Products == null ? new List<int>() : v.Products.Select(e => e.ProductId).ToList(),
            }).OrderByDescending(e => e.Id)
            .ToListAsync(cancellationToken);
    }    
}