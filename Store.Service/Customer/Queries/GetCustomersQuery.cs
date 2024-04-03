using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;

namespace Store.Service.Queries;

public class GetCustomersQueryHandler(AppDbContext appDbContext) : IRequestHandler<IList<CustomerResponse>>
{
    public async Task<IList<CustomerResponse>> Handle(CancellationToken cancellationToken = default)
    {
        return await appDbContext.Customers
            .AsNoTracking()
            .Select(e => new  CustomerResponse
            {
                CreatedDate = e.CreatedDate,
                UdateDate = e.UpdateDate,
                Id = e.Id,
                Description = e.Description,
                Name = e.Name,
            }).OrderByDescending(e => e.Id)
            .ToListAsync(cancellationToken);
    }
}
