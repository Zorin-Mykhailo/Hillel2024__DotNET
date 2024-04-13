using Microsoft.EntityFrameworkCore;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;

public record DeleteOrderLineCommand(int OrderId, int ProductId);

public class DeleteOrderLineCommandHandler(AppDbContext appDbContext) : IRequestHandler<DeleteOrderLineCommand, bool>
{
    public async Task<bool> Handle(DeleteOrderLineCommand request, CancellationToken cancellationToken)
    {
        OrderLine? orderLine = await GetOrderLineAsync(request.OrderId, request.ProductId, cancellationToken);
        if(orderLine == null) return false;

        appDbContext.Remove(orderLine);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }


    private async Task<OrderLine?> GetOrderLineAsync(int orderId, int productId, CancellationToken cancellationToken = default)
        => await appDbContext.OrderLines.SingleOrDefaultAsync(e => e.OrderId == orderId && e.ProductId == productId, cancellationToken);
}