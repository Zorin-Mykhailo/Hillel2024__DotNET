using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;

namespace Store.Service.Queries;

internal class GetCategoryByIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<int, CategoryResponse?>
{
    public async Task<CategoryResponse?> Handle(int categoryId, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Categories
            .AsNoTracking()
            .Where(e => e.Id == categoryId)
            .Select(e => new CategoryResponse
            {
                CreatedDate = e.CreatedDate,
                UpdateDate = e.UpdateDate,
                Id = e.Id,
                Description = e.Description,
                Name = e.Name
            }).SingleOrDefaultAsync(cancellationToken);
    }
}