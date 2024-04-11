using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.DTOs;
using HW05.MovieManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Movies;

public class MovieQueryGetById : IRequest<MovieDTO?>
{
    public int Id { get; set; }

    public class Handler(IAppDbContext appDbContext) : IRequestHandler<MovieQueryGetById, MovieDTO?>
    {
        public async Task<MovieDTO?> Handle(MovieQueryGetById query, CancellationToken cancellationToken = default)
        {
            Movie? movie = await appDbContext.Movies.Where(e => e.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            return MovieDTO.FromEntity(movie);
        }
    }
}