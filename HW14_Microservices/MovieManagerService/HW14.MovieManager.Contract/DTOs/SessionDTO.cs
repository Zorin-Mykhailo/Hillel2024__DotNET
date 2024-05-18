using HW14.MovieManager.Data.Entities;

namespace HW14.MovieManager.Contract.DTOs;

public record SessionDTO
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public virtual MovieDTO? Movie { get; set; } = default!;

    public string RoomName { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }

    public static SessionDTO? FromEntity(Session? entity)
    {
        if(entity == null) return null;

        return new()
        {
            Id = entity.Id,
            MovieId = entity.MovieId,
            Movie = MovieDTO.FromEntity(entity.Movie),
            RoomName = entity.RoomName,
            StartAt = entity.StartAt,
        };
    }
}