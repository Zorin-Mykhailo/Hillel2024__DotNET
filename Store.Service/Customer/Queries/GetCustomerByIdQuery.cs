using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;

namespace Store.Service.Queries;

public class GetCustomerByIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<int, CustomerResponse?>
{
    public async Task<CustomerResponse?> Handle(int customerId, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Customers
            .AsNoTracking()
            .Where(e => e.Id == customerId)
            .Select(e => new CustomerResponse
            {
                CreatedDate = e.CreatedDate,
                LastModifiedDate = e.LastModifiedDate,
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,

            }).SingleOrDefaultAsync(cancellationToken);
    }
}