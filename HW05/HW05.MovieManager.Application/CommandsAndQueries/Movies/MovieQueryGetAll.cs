using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Movies;

public class MovieQueryGetAll : IRequest<IEnumerable<MovieDTO>>
{
    public class Handler(IAppDbContext appDbContext) : IRequestHandler<MovieQueryGetAll, IEnumerable<MovieDTO>>
    {
        public async Task<IEnumerable<MovieDTO>> Handle(MovieQueryGetAll query, CancellationToken cancellationToken = default)
        {
            IEnumerable<MovieDTO> movies = await appDbContext.Movies.Select(e => MovieDTO.FromEntity(e)!).ToListAsync(cancellationToken);
            return movies;
        }
    }
}