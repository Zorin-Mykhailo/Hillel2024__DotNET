using Microsoft.EntityFrameworkCore;
using Store.Contract.Requests;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;

public class DeleteOrderCommandHandler(AppDbContext appDbContext) : IRequestHandler<DeleteOrderRequest, bool>
{
    public async Task<bool> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        Order? order = await GetOrderAsync(request.OrderId, cancellationToken);
        if(order == null) return false;

        appDbContext.Remove(order);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task<Order?> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
        => await appDbContext.Orders.SingleOrDefaultAsync(e => e.Id == orderId, cancellationToken);
}
