using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"{nameof(Order)}s")]
public record Order : BaseEntity
{
    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public ICollection<OrderLine> OrderLines { get; set; } = [];

    public double TotalSum { get; set; }

    public string? Notes { get; set; }
}



internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> e)
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
        e.Property(e => e.CustomerId).IsRequired();
        e.Property(e => e.TotalSum).IsRequired();

        e.HasOne(o => o.Customer)
            .WithMany(m => m.Orders)
            .HasForeignKey(f => f.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}