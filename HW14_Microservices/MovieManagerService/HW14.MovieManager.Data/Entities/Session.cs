using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW14.MovieManager.Data.Entities;

[Table($"{nameof(Session)}s")]
public class Session : BaseEntity
{
    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = default!;

    public string RoomName { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }
}



internal class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> e)
    {
        e.HasKey(x => x.Id);
        e.Property(e => e.MovieId)
            .IsRequired();
        e.Property(e => e.RoomName)
            .IsRequired()
            .HasMaxLength(255);
        e.Property(e => e.StartAt)
            .IsRequired();
    }
}