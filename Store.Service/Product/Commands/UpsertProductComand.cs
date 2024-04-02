using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
using Store.Data.Entities;

namespace Store.Service.Commands;

public class UpsertProductComand
{
    public int Id { get; set; }
    
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }

    public double CurrentPricePerUnit { get; set; }


    public Product UpsertProduct()
    {
        Product product = new ()
        {
            CategoryId = CategoryId,
            Name = Name,
            Description = Description,
            CurrentPricePerUnit = CurrentPricePerUnit,          
        };
        return product;
    }
}

public class UpsertProductCommandHandler(AppDbContext appDbContext) : IRequestHandler<UpsertProductComand, ProductResponse>
{
    public async Task<ProductResponse> Handle(UpsertProductComand request, CancellationToken cancellationToken = default)
    {
        Product? product = await GetProductAsync(request.Id, cancellationToken);

        if(product == null)
        {
            product = request.UpsertProduct();
            product.CreatedDate = DateTime.Now;
            await appDbContext.AddAsync(product, cancellationToken);
        }
        else
        {
            product.CategoryId = request.CategoryId;
            product.Name = request.Name;
            product.Description = request.Description;
            product.CurrentPricePerUnit = request.CurrentPricePerUnit;
        }

        product.LastModifiedDate = DateTime.Now;
        await appDbContext.SaveChangesAsync(cancellationToken);

        return new ProductResponse
        {
            CreatedDate = product.CreatedDate,
            LastModifiedDate = product.LastModifiedDate,
            Id = product.Id,
            CategoryId = product.CategoryId,
            Name = product.Name,
            Description = product.Description,
            CurrentPricePerUnit = product.CurrentPricePerUnit
        };
    }

    private async Task<Product?> GetProductAsync(int productId, CancellationToken cancellationToken = default)
        => await appDbContext.Products.SingleOrDefaultAsync(e => e.Id == productId, cancellationToken);
}