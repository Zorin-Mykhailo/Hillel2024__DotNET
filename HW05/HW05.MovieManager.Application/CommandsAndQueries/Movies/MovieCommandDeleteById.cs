using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Movies;

public class MovieCommandDeleteById : IRequest<bool>
{
    public int Id { get; set; }

    public class Handler(IAppDbContext appDbContext) : IRequestHandler<MovieCommandDeleteById, bool>
    {
        public async Task<bool> Handle(MovieCommandDeleteById command, CancellationToken cancellationToken = default)
        {
            Movie? movie = await appDbContext.Movies.Where(e => e.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if(movie == null) return false;

            appDbContext.Movies.Remove(movie);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}