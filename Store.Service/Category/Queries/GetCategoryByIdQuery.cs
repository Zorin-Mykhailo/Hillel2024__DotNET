using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;

namespace Store.Service.Queries;

internal class GetCategoryByIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<int, CategoryResponse?>
{
    public async Task<CategoryResponse?> Handle(int categoryId, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Categories
            .AsNoTracking()
            .Where(e => e.Id == categoryId)
            .Select(e => new CategoryResponse
            {
                CreatedDate = e.CreatedDate,
                LastModifiedDate = e.LastModifiedDate,
                Id = e.Id,
                Description = e.Description,
                Name = e.Name,
                //TODO products
                //ProductsId = e.Products == null ? new List<int>() : e.Products.Select(e => e.ProductId).ToList(),
            }).SingleOrDefaultAsync(cancellationToken);
    }
}