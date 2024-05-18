using HW14.MovieManager.Contract.DTOs;
using HW14.MovieManager.Data.Context;
using HW14.MovieManager.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieManager.Service.CommandsAndQueries.Movies;

public record MovieQueryGetById(int Id) : IRequest<MovieDTO?>
{
    public class Handler(AppDbContext appDbContext) : IRequestHandler<MovieQueryGetById, MovieDTO?>
    {
        public async Task<MovieDTO?> Handle(MovieQueryGetById query, CancellationToken cancellationToken = default)
        {
            Movie? movie = await appDbContext.Movies.Where(e => e.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            return MovieDTO.FromEntity(movie);
        }
    }
}