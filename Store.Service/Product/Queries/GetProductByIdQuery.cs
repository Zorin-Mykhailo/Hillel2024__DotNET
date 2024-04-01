using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
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
                LastModifiedDate = v.LastModifiedDate,
                Description = v.Description,
                Id = v.Id,
                Name = v.Name,
                CurrentPricePerUnit = v.CurrentPricePerUnit,

                //TODO Categories
                //TODO Orders
            }).SingleOrDefaultAsync(cancellationToken);
    }
}