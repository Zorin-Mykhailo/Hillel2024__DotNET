using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;

public class UpsertCustomerCommand
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }


    public Customer UpsertCustomer()
    {
        Customer customer = new ()
        {
            Name = Name,
            Description = Description
        };
        return customer;
    }
}

public class UpsertCustomerCommandHandler(AppDbContext appDbContext) : IRequestHandler<UpsertCustomerCommand, CustomerResponse>
{
    public async Task<CustomerResponse> Handle(UpsertCustomerCommand request, CancellationToken cancellationToken = default)
    {
        Customer? customer = await GetCustomerAsync(request.Id, cancellationToken);

        if(customer == null)
        {
            customer = request.UpsertCustomer();
            customer.CreatedDate = DateTime.UtcNow;
            await appDbContext.AddAsync(customer, cancellationToken);
        }
        else
        {
            customer.Name = request.Name;
            customer.Description = request.Description;
        }

        customer.UpdateDate = DateTime.UtcNow;
        await appDbContext.SaveChangesAsync(cancellationToken);

        return new CustomerResponse
        {
            CreatedDate = customer.CreatedDate,
            UdateDate =  customer.UpdateDate,
            Id = customer.Id,
            Name = customer.Name,
            Description = customer.Description
        };
    }

    private async Task<Customer?> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default)
        => await appDbContext.Customers.SingleOrDefaultAsync(e => e.Id == customerId, cancellationToken);
}