﻿using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;

namespace Store.Service.Queries;

public class GetOrderByIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<int, OrderResponse?>
{
    public async Task<OrderResponse?> Handle(int orderId, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Orders
            .AsNoTracking()
            .Where(e => e.Id == orderId)
            .Include(e => e.Customer)
            .Include(e => e.OrderLines)            
            .Select(e => new OrderResponse(e.Id, e.CreatedDate, e.UpdateDate)
            {
                Notes = e.Notes,
                CustomerId = e.CustomerId,
                Customer = new CustomerResponse()
                {
                    CreatedDate = e.Customer!.CreatedDate,
                    UdateDate = e.Customer!.UpdateDate,
                    Id = e.Customer!.Id,
                    Name = e.Customer!.Name,
                    Description = e.Customer!.Description,
                },
                TotalSum = e.TotalSum,

            }).SingleOrDefaultAsync(cancellationToken);
    }

}