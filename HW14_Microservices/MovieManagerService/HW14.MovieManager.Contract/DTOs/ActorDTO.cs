namespace HW14.MovieManager.Contract.DTOs;

public record ActorDTO
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public string Description { get; set; } = string.Empty;
}