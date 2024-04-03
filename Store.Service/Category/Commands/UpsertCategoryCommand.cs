using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;

public class UpsertCategoryCommand
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }


    public Category UpsertCategory()
    {
        Category category = new ()
        {
            Name = Name,
            Description = Description
        };
        return category;
    }
}

public class UpsertCategoryCommandHandler(AppDbContext appDbContext) : IRequestHandler<UpsertCategoryCommand, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(UpsertCategoryCommand request, CancellationToken cancelationToken = default)
    {
        Category? category = await GetCategoryAsync(request.Id, cancelationToken);
         
        if(category == null)
        {
            category = request.UpsertCategory();
            category.CreatedDate = DateTime.UtcNow;
            await appDbContext.AddAsync(category, cancelationToken);
        }
        else
        {
            category.Name = request.Name;
            category.Description = request.Description;
        }

        category.UpdateDate = DateTime.UtcNow;
        await appDbContext.SaveChangesAsync(cancelationToken);

        return new CategoryResponse
        {
            CreatedDate = category.CreatedDate,
            UpdateDate = category.UpdateDate,
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        };
    }

    private async Task<Category?> GetCategoryAsync(int categoryId, CancellationToken cancelationToken = default)
        => await appDbContext.Categories.SingleOrDefaultAsync(e => e.Id == categoryId, cancelationToken);
}
