using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Movies;

public record MovieCommandUpdate : IRequest<bool>
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }    
}




public record MovieCommandUpdateSingle : MovieCommandUpdate
{
    public int Id { get; set; }

    public MovieCommandUpdateSingle(int id, MovieCommandUpdate updateCommand)
    {
        
        Id = id;
        Title = updateCommand.Title;
        Description = updateCommand.Description;
        ReleaseDate = updateCommand.ReleaseDate;
    }


    public class Handler(IAppDbContext appDbContext) : IRequestHandler<MovieCommandUpdateSingle, bool>
    {
        public async Task<bool> Handle(MovieCommandUpdateSingle command, CancellationToken cancellationToken = default)
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