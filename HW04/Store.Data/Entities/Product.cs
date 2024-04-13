using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"{nameof(Product)}s")]
public record Product : BaseEntityWithNameAndDescription
{
    public double CurrentPricePerUnit { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = default!;

    public ICollection<OrderLine>? Orders { get; set; }
}



internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> e)
    {
        e.Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow)
            .ValueGeneratedOnAdd();
        e.Property(e => e.UpdateDate)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow)
            .ValueGeneratedOnAddOrUpdate();
        e.HasKey(e => e.Id);
        e.Property(e => e.Id).ValueGeneratedOnAdd();
        e.Property(e => e.Name).IsRequired();
        e.Property(e => e.CurrentPricePerUnit).IsRequired();
        e.Property(e => e.CategoryId).IsRequired();
    }
}