using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;
public record DeleteOrderLinesCommand(int OrderId);

public class DeleteOrderLinesCommandHandler(AppDbContext appDbContext) : IRequestHandler<DeleteOrderLinesCommand, bool>
{
    public async Task<bool> Handle(DeleteOrderLinesCommand request, CancellationToken cancellationToken = default)
    {
        List<OrderLine> orderLines = await GetOrderLinesAsync(request.OrderId, cancellationToken);
        if(orderLines.IsNullOrEmpty()) return false;

        appDbContext.RemoveRange(orderLines);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }


    private async Task<List<OrderLine>> GetOrderLinesAsync(int orderId, CancellationToken cancellationToken = default)
        => await appDbContext.OrderLines.Where(e => e.OrderId == orderId).ToListAsync(cancellationToken);
}
