using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;

namespace Store.Service.Queries;

public class GetProductsQueryHandler(AppDbContext appDbContext) : IRequestHandler<IList<ProductResponse>>
{
    public async Task<IList<ProductResponse>> Handle(CancellationToken cancellationToken = default)
    {
        return await appDbContext.Products
            .AsNoTracking()
            .Select(e => new ProductResponse
            {
                CreatedDate = e.CreatedDate,
                UdateDate = e.UpdateDate,
                Id = e.Id,
                Description = e.Description,
                Name = e.Name,
                CurrentPricePerUnit = e.CurrentPricePerUnit,
                CategoryId = e.CategoryId,
            }).OrderByDescending(e => e.Id)
            .ToListAsync(cancellationToken);
    }
}
