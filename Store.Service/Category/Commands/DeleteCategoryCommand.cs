using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;

namespace Store.Service.Commands;

public record DeleteCategoryCommand(int CategoryId);

public class DeleteCategoryCommandHandler(AppDbContext appDbContext) : IRequestHandler<DeleteCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await GetCategoryAsync(request.CategoryId, cancellationToken);
        if(category == null) return false;

        appDbContext.Remove(category);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task<Category?> GetCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        => await appDbContext.Categories.SingleOrDefaultAsync(e => e.Id == categoryId, cancellationToken);
}