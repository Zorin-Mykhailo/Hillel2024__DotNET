using HW05.MovieManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HW05.MovieManager.Persistence.Configuration;
internal class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> e)
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