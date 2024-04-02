using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;

namespace Store.Service.Commands;

public record DeleteOrderCommand(int OrderId);

public class DeleteOrderCommandHandler(AppDbContext appDbContext) : IRequestHandler<DeleteOrderCommand, bool>
{
    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
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
