using Microsoft.EntityFrameworkCore;
using Store.Data.Db;
using Store.Data.Entities;

namespace Store.Service.Commands;


public record DeleteProductCommand(int ProductId);

public class DeleteProductCommandHandler(AppDbContext appDbContext) : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await GetProductAsync(request.ProductId, cancellationToken);
        if(product == null) return false;

        appDbContext.Remove(product);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task<Product?> GetProductAsync(int productId, CancellationToken cancellationToken = default)
        => await appDbContext.Products.SingleOrDefaultAsync(e => e.Id == productId, cancellationToken);
}