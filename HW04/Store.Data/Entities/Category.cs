using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table("Categories")]
public record Category : BaseEntityWithNameAndDescription
{
    public ICollection<Product>? Products { get; set; }
}

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> e)
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
        e.Property(e => e.Name).IsRequired().HasMaxLength(250);
    }
}