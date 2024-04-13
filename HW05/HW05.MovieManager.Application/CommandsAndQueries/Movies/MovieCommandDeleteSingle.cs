using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Movies;

public record MovieCommandDeleteSingle(int Id) : IRequest<bool>
{
    public class Handler(IAppDbContext appDbContext) : IRequestHandler<MovieCommandDeleteSingle, bool>
    {
        public async Task<bool> Handle(MovieCommandDeleteSingle command, CancellationToken cancellationToken = default)
        {
            Movie? movie = await appDbContext.Movies.Where(e => e.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if(movie == null) return false;

            appDbContext.Movies.Remove(movie);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}