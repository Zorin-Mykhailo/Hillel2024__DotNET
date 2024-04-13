namespace HW05.MovieManager.Domain.Entities;

public class Session : BaseEntity
{
    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = default!;

    public string RoomName { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }
}