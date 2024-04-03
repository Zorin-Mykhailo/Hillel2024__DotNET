using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;

namespace Store.Service.Queries;

public class GetCategoriesQueryHandler(AppDbContext appDbContext) : IRequestHandler<IList<CategoryResponse>>
{
    public async Task<IList<CategoryResponse>> Handle(CancellationToken cancellationToken = default)
    {
        return await appDbContext.Categories
            .AsNoTracking()
            .Select(v => new CategoryResponse
            {
                CreatedDate = v.CreatedDate,
                UpdateDate = v.UpdateDate,
                Id = v.Id,
                Description = v.Description,
                Name = v.Name,                
            }).OrderByDescending(e => e.Id)
            .ToListAsync(cancellationToken);
    }    
}