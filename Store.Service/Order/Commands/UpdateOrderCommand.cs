using Microsoft.EntityFrameworkCore;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;

public class UpdateOrderCommandHandler(AppDbContext appDbContext) : IRequestHandler<int, UpdateOrderRequest, OrderResponse?>
{
    public async Task<OrderResponse?> Handle(int orderId, UpdateOrderRequest request, CancellationToken cancellationToken = default)
    {
        Order? order = await GetOrderByIdAsync(orderId, cancellationToken);
        if(order == null) return null;

        order.Notes = request.Notes;
        await appDbContext.SaveChangesAsync(cancellationToken);

        return new OrderResponse(order.Id, order.CreatedDate, order.UpdateDate)
        {
            CustomerId = order.CustomerId,
            TotalSum = order.TotalSum,
            Notes = order.Notes
        };
    }

    private async Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellation = default)
        => await appDbContext.Orders.SingleOrDefaultAsync(e => e.Id == orderId);
}
