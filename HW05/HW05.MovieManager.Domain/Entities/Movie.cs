namespace HW05.MovieManager.Domain.Entities;

public class Movie : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }
}
