using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Movies;

public class MovieCommandUpdateById : IRequest<bool>
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public class Handler(IAppDbContext appDbContext) : IRequestHandler<MovieCommandUpdateById, bool>
    {
        public async Task<bool> Handle(MovieCommandUpdateById command, CancellationToken cancellationToken = default)
        {
            Movie? movie = await appDbContext.Movies.Where(e => e.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if(movie == null) return false;

            movie.Title = command.Title;
            movie.Description = command.Description;
            movie.ReleaseDate = command.ReleaseDate;
            
            await appDbContext.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}