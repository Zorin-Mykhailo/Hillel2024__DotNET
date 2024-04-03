using Microsoft.EntityFrameworkCore;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;

//public class InsertOrderCommand
//{
//    public int OrderId { get; set; }

//    public int CustomerId { get; set; }

//    public string? Notes { get; set; } = string.Empty;

//    public Order UpsertOrder()
//    {
//        Order order = new ()
//        {
//            CustomerId = CustomerId,
//            Notes = Notes
//        };
//        return order;
//    }
//}

public class InsertOrderCommandHandler(AppDbContext appDbContext) : IRequestHandler<InsertOrderRequest, OrderResponse>
{
    public async Task<OrderResponse> Handle(InsertOrderRequest request, CancellationToken cancellationToken = default)
    {
        Order order = new ()
        {
            CustomerId = request.CustomerId,
            Notes = request.Notes
        };
        
        await appDbContext.SaveChangesAsync(cancellationToken);

        return new OrderResponse(order.Id, order.CreatedDate, order.UpdateDate)
        {
            CustomerId = order.CustomerId,
            Notes = order.Notes
        };
    }
}