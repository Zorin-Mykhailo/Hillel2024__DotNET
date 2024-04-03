using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"{nameof(Customer)}s")]
public record Customer : BaseEntityWithNameAndDescription
{
    public ICollection<Order>? Orders { get; set; }
}

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> e)
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