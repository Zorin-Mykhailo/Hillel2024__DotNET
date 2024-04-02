using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;

namespace Store.Service.Queries;

public class GetOrdersQueryHandler(AppDbContext appDbContext) : IRequestHandler<IList<OrderResponse>>
{
    public async Task<IList<OrderResponse>> Handle(CancellationToken cancellationToken = default)
    {
        return await appDbContext.Orders
            .AsNoTracking()
            .Include(e => e.Customer)
            .Select(e => new OrderResponse
            {
                CreatedDate = e.CreatedDate,
                LastModifiedDate = e.LastModifiedDate,
                Id = e.Id,
                Notes = e.Notes,
                CustomerId = e.CustomerId,
                Customer = new CustomerResponse ()
                {
                     CreatedDate = e.Customer!.CreatedDate,
                     LastModifiedDate = e.Customer!.LastModifiedDate,
                     Id = e.Customer!.Id,
                     Name = e.Customer!.Name,
                     Description = e.Customer!.Description,
                }
            }).OrderByDescending(e => e.Id)
            .ToListAsync(cancellationToken);
    }
}