using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;

namespace Store.Service.Commands;

public record DeleteCustomerCommand(int CustomerId);

public class DeleteCustomerCommandHandler(AppDbContext appDbContext) : IRequestHandler<DeleteCustomerCommand, bool>
{
    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await GetCustomerAsync(request.CustomerId, cancellationToken);
        if(customer == null) return false;

        appDbContext.Remove(customer);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task<Customer?> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default)
        => await appDbContext.Customers.SingleOrDefaultAsync(e => e.Id == customerId, cancellationToken);
}