using Microsoft.EntityFrameworkCore;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;

public class InsertOrderCommandHandler(AppDbContext appDbContext) : IRequestHandler<InsertOrderRequest, OrderResponse>
{
    public async Task<OrderResponse> Handle(InsertOrderRequest request, CancellationToken cancellationToken = default)
    {
        Order order = new ()
        {
            CustomerId = request.CustomerId,
            Notes = request.Notes
        };

        await appDbContext.AddAsync(order, cancellationToken);        
        await appDbContext.SaveChangesAsync(cancellationToken);

        return new OrderResponse
        {
            CreatedDate = order.CreatedDate,
            UdateDate = order.UpdateDate,
            Id = order.Id,
            CustomerId = order.CustomerId,
            Notes = order.Notes
        };
    }
}