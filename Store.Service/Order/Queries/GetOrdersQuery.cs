using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;

namespace Store.Service.Queries;

public class GetOrdersQueryHandler(AppDbContext appDbContext) : IRequestHandler<IList<OrderResponse>>
{
    public async Task<IList<OrderResponse>> Handle(CancellationToken cancellationToken = default)
    {
        return await appDbContext.Orders
            .AsNoTracking()
            .Include(e => e.Customer)
            .Select(e => new OrderResponse(e.Id, e.CreatedDate, e.UpdateDate)
            {
                Notes = e.Notes,
                CustomerId = e.CustomerId,
                Customer = new CustomerResponse ()
                {
                     CreatedDate = e.Customer!.CreatedDate,
                     UdateDate = e.Customer!.UpdateDate,
                     Id = e.Customer!.Id,
                     Name = e.Customer!.Name,
                     Description = e.Customer!.Description,
                }
            }).OrderByDescending(e => e.Id)
            .ToListAsync(cancellationToken);
    }
}