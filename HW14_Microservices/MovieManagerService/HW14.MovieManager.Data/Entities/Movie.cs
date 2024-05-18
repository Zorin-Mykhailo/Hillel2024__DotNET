using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW14.MovieManager.Data.Entities;

[Table($"{nameof(Movie)}s")]
public class Movie : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    //public ICollection<int>? ActorsIds { get; set; }
}



internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> e)
    {
        e.HasKey(x => x.Id);
        e.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(255);
        e.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500);
        e.Property(e => e.ReleaseDate)
            .IsRequired();
    }
}