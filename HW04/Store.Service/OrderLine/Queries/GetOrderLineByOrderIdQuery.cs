using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;

namespace Store.Service.Queries;

public class GetOrderLineByOrderIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<(int orderId, int productId), OrderLineResponse?>
{
    public async Task<OrderLineResponse?> Handle((int orderId, int productId) param, CancellationToken cancellationToken = default)
    {
        return await appDbContext.OrderLines
            .AsNoTracking()
            .Where(x => x.OrderId == param.orderId && x.ProductId == param.productId)
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
            }).SingleOrDefaultAsync(cancellationToken);
    }
}
