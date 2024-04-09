using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;
using System;

namespace Store.Service.Queries;

public class GetProductByIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<int, ProductResponse?>
{
    public async Task<ProductResponse?> Handle(int productId, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Products
            .AsNoTracking()
            .Where(e => e.Id == productId)
            .Select(v => new ProductResponse
            {
                CreatedDate = v.CreatedDate,
                UdateDate = v.UpdateDate,
                Description = v.Description,
                Id = v.Id,
                CategoryId = v.CategoryId,
                Name = v.Name,
                CurrentPricePerUnit = v.CurrentPricePerUnit,
            }).SingleOrDefaultAsync(cancellationToken);
    }
}