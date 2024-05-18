namespace HW14.MovieManager.Contract.DTOs;

public record ActorMinimalInfoDTO
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}