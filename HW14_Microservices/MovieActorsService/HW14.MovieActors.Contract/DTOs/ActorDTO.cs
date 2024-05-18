using HW14.MovieActors.Data.Entities;

namespace HW14.MovieActors.Contract.DTOs;

public record ActorDTO
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public string Description { get; set; } = string.Empty;

    public static ActorDTO? FromEntity(Actor? entity)
    {
        if(entity == null) return null;

        return new()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            DateOfBirth = entity.DateOfBirth,
            Description = entity.Description,
        };
    }
}