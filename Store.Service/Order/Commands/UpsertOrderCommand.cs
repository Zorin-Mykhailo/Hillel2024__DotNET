using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
using Store.Data.Entities;

namespace Store.Service.Commands;

public class UpsertOrderCommand
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? Notes { get; set; } = string.Empty;

    public Order UpsertOrder()
    {
        Order order = new ()
        {
            CustomerId = CustomerId,
            Notes = Notes
        };
        return order;
    }
}

public class UpsertOrderCommandHandler(AppDbContext appDbContext) : IRequestHandler<UpsertOrderCommand, OrderResponse>
{
    public async Task<OrderResponse> Handle(UpsertOrderCommand request, CancellationToken cancellationToken = default)
    {
        Order? order = await GetOrderAsync(request.Id, cancellationToken);

        if(order == null)
        {
            order = request.UpsertOrder();
            order.CreatedDate = DateTime.Now;
            await appDbContext.AddAsync(order, cancellationToken);
        }
        else
        {
            order.CustomerId = request.CustomerId;
            order.Notes = request.Notes;
        }

        order.LastModifiedDate = DateTime.Now;
        await appDbContext.SaveChangesAsync(cancellationToken);

        return new OrderResponse
        {
            CreatedDate = order.CreatedDate,
            LastModifiedDate = order.LastModifiedDate,
            Id = order.Id,
            CustomerId = order.CustomerId,
            Notes = order.Notes
        };
    }


    private async Task<Order?> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
        => await appDbContext.Orders.SingleOrDefaultAsync(e => e.Id == orderId, cancellationToken);
}