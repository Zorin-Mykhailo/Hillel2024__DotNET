using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
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
            customer.CreatedDate = DateTime.Now;
            await appDbContext.AddAsync(customer, cancellationToken);
        }
        else
        {
            customer.Name = request.Name;
            customer.Description = request.Description;
        }

        customer.LastModifiedDate = DateTime.Now;
        await appDbContext.SaveChangesAsync(cancellationToken);

        return new CustomerResponse
        {
            CreatedDate = customer.CreatedDate,
            LastModifiedDate =  customer.LastModifiedDate,
            Id = customer.Id,
            Name = customer.Name,
            Description = customer.Description
        };
    }

    private async Task<Customer?> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default)
        => await appDbContext.Customers.SingleOrDefaultAsync(e => e.Id == customerId, cancellationToken);
}