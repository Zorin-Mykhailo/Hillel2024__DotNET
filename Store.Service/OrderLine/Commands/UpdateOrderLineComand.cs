using Microsoft.EntityFrameworkCore;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;

public class UpdateOrderLineComand(AppDbContext appDbContext) : IRequestHandler<(int orderId, int productId), UpdateOrderLineRequest, OrderLineResponse?>
{
    public async Task<OrderLineResponse?> Handle((int orderId, int productId) param, UpdateOrderLineRequest request, CancellationToken cancellationToken = default)
    {
        OrderLine? orderLine = await GetOrderLineAsync(param.orderId, param.productId);
        if(orderLine == null) return null;

        orderLine.UpdateDate = DateTime.UtcNow;        
        orderLine.Notes = request.Notes;
        orderLine.ProductAmount = request.ProductAmount;
        orderLine.PricePerUnit = await GetProductPrice(orderLine.ProductId, cancellationToken) ?? 0.0;
        orderLine.TotalSum = orderLine.PricePerUnit * orderLine.ProductAmount;

        await UpdateOrderTotalSum(orderLine, cancellationToken);

        await appDbContext.SaveChangesAsync(cancellationToken);

        return new OrderLineResponse
        {
            CreatedDate = orderLine.CreatedDate,
            UpdateDate = orderLine.UpdateDate,
            OrderId = orderLine.OrderId,
            ProductId = orderLine.ProductId,
            Notes = orderLine.Notes,
            ProductAmount = orderLine.ProductAmount,
            PricePerUnit = orderLine.PricePerUnit,
            TotalSum = orderLine.TotalSum,
        };
    }

    private async Task<OrderLine?> GetOrderLineAsync(int orderId, int productId, CancellationToken cancellationToken = default)
        => await appDbContext.OrderLines.SingleOrDefaultAsync(e => e.OrderId == orderId && e.ProductId == productId, cancellationToken);

    private async Task<double?> GetProductPrice(int productId, CancellationToken cancellationToken = default)
    {
        Product? product = await appDbContext.Products.SingleOrDefaultAsync(e => e.Id == productId);
        return product?.CurrentPricePerUnit ?? null;
    }

    private async Task UpdateOrderTotalSum(OrderLine orderLine, CancellationToken cancellationToken = default)
    {
        Order? order = await appDbContext.Orders.Where(e => e.Id == orderLine.OrderId).Include(o => o.OrderLines).SingleOrDefaultAsync(cancellationToken);
        if(order == null) return;
        order.TotalSum = order.OrderLines.Sum(e => e.TotalSum);
    }
}