using Microsoft.EntityFrameworkCore;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;

public class InsertOrderLineCommandHandler(AppDbContext appDbContext) : IRequestHandler<int, InsertOrderLineRequest, OrderLineResponse>
{
    public async Task<OrderLineResponse> Handle(int orderId, InsertOrderLineRequest request, CancellationToken cancellationToken = default)
    {
        OrderLine orderLine = new ();
        orderLine.OrderId = orderId;
        orderLine.ProductId = request.ProductId;
        orderLine.Notes = request.Notes;
        orderLine.ProductAmount = request.ProductAmount;
        orderLine.PricePerUnit = await GetProductPrice(orderLine.ProductId, cancellationToken) ?? 0.0;
        orderLine.TotalSum = orderLine.PricePerUnit * orderLine.ProductAmount;
        
        await UpdateOrderTotalSum(orderLine, cancellationToken);

        await appDbContext.AddAsync(orderLine, cancellationToken);
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