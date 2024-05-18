using HW14.MovieManager.Data.Context;
using HW14.MovieManager.Data.Entities;
using MediatR;

namespace HW14.MovieManager.Service.CommandsAndQueries.Movies;
public record MovieCommandCreate : IRequest<int>
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<MovieCommandCreate, int>
    {
        public async Task<int> Handle(MovieCommandCreate command, CancellationToken cancellationToken = default)
        {
            Movie movie = new ()
            {
                Title = command.Title,
                Description = command.Description,
                ReleaseDate = command.ReleaseDate,
            };

            await appDbContext.Movies.AddAsync(movie, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
    }
}