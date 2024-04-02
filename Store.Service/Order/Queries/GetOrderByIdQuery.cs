using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;

namespace Store.Service.Queries;

public class GetOrderByIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<int, OrderResponse?>
{
    public async Task<OrderResponse?> Handle(int orderId, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Orders
            .AsNoTracking()
            .Where(e => e.Id == orderId)
            .Include(e => e.Customer)
            .Select(e => new OrderResponse
            {
                CreatedDate = e.CreatedDate,
                LastModifiedDate = e.LastModifiedDate,
                Id = e.Id,
                Notes = e.Notes,
                CustomerId = e.CustomerId,
                Customer = new CustomerResponse()
                {
                    CreatedDate = e.Customer!.CreatedDate,
                    LastModifiedDate = e.Customer!.LastModifiedDate,
                    Id = e.Customer!.Id,
                    Name = e.Customer!.Name,
                    Description = e.Customer!.Description,
                }
            }).SingleOrDefaultAsync(cancellationToken);
    }

}
