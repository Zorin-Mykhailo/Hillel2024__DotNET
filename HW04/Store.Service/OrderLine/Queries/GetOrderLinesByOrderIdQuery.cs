using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;

namespace Store.Service.Queries;

public class GetOderLinesByOrderIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<int, IList<OrderLineResponse>>
{
    public async Task<IList<OrderLineResponse>> Handle(int orderId, CancellationToken cancellationToken = default)
    {
        return await appDbContext.OrderLines
            .AsNoTracking()
            .Where(x => x.OrderId == orderId)
            .Select(e => new OrderLineResponse
            {
                CreatedDate = e.CreatedDate,
                UpdateDate = e.UpdateDate,
                OrderId = e.OrderId,
                ProductId = e.ProductId,
                Notes = e.Notes,
                PricePerUnit = e.PricePerUnit,
                ProductAmount = e.ProductAmount,
                TotalSum = e.TotalSum,
            }).OrderByDescending(e => e.ProductId)
            .ToListAsync(cancellationToken);
    }
}
