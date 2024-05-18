using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW14.MovieActors.Data.Entities;

[Table($"{nameof(Actor)}s")]
public class Actor : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public string Description { get; set; } = string.Empty;
}



internal class ActorConfiguration : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> e)
    {
        e.HasKey(x => x.Id);
        e.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        e.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50);
        e.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(1024);
    }
}