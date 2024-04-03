using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;

public class UpsertOrderLineCommand
{
    public int ProductId { get; set; }

    public string? Notes { get; set; } = string.Empty;

    public double ProductAmount { get; set; }

    public OrderLine UpsertOrderLine(int orderId)
    {
        OrderLine orderLine = new()
        {
            OrderId = orderId,
            ProductId = ProductId,
            Notes = Notes,
            ProductAmount = ProductAmount
        };
        return orderLine;
    }
}

public class UpsertOrderLineCommandHandler(AppDbContext appDbContext) : IRequestHandler<int, UpsertOrderLineCommand, OrderLineResponse>
{
    public async Task<OrderLineResponse> Handle(int orderId, UpsertOrderLineCommand request, CancellationToken cancellationToken = default)
    {
        OrderLine? orderLine = await GetOrderLineAsync(orderId, request.ProductId, cancellationToken);

        if(orderLine == null)
        {
            orderLine = request.UpsertOrderLine(orderId);
            await appDbContext.AddAsync(orderLine, cancellationToken);
        }

        orderLine.Notes = request.Notes;
        orderLine.ProductAmount = request.ProductAmount;
        orderLine.PricePerUnit = await GetProductPrice(orderLine, cancellationToken) ?? 0.0;
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

    private async Task UpdateOrderTotalSum(OrderLine orderLine, CancellationToken cancellationToken = default)
    {
        Order? order = await appDbContext.Orders.Where(e => e.Id == orderLine.OrderId).Include(o => o.OrderLines).SingleOrDefaultAsync(cancellationToken);
        if(order == null) return;
        order.TotalSum = order.OrderLines.Sum(e => e.TotalSum);
            
    }

    private async Task<double?> GetProductPrice(OrderLine orderLine, CancellationToken cancellationToken = default)
    {
        Product? product = await appDbContext.Products.SingleOrDefaultAsync(e => e.Id == orderLine.ProductId);
        return product?.CurrentPricePerUnit ?? null;
    }

    private async Task<OrderLine?> GetOrderLineAsync(int orderId, int productId, CancellationToken cancellationToken = default)
        => await appDbContext.OrderLines.SingleOrDefaultAsync(e => e.OrderId ==  orderId && e.ProductId == productId, cancellationToken);
}