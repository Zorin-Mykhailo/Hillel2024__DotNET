using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;

namespace Store.Service.Queries;

internal class GetCategoryByIdWithProductsQuery(AppDbContext appDbContext) : IRequestHandler<int, CategoryWithProductsResponse?>
{
    public async Task<CategoryWithProductsResponse?> Handle(int categoryId, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Categories
            .AsNoTracking()
            .Where(e => e.Id == categoryId)
            .Include(e => e.Products)
            .Select(e => new CategoryWithProductsResponse
            {
                CreatedDate = e.CreatedDate,
                UdateDate = e.UpdateDate,
                Id = e.Id,
                Description = e.Description,
                Name = e.Name,
                Products = e.Products.Select(p => new ProductResponse
                {
                     CreatedDate = p.CreatedDate,
                     UdateDate = p.UpdateDate,
                     Id = p.Id,
                     CategoryId = categoryId,
                     Name = p.Name,
                     CurrentPricePerUnit = p.CurrentPricePerUnit,
                     Description = p.Description,
                }).ToList()
            }).SingleOrDefaultAsync(cancellationToken);
    }
}