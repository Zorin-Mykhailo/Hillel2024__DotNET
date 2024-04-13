using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Movies;

public record MovieQueryGetAll : IRequest<ICollection<MovieDTO>>
{
    public class Handler(IAppDbContext appDbContext) : IRequestHandler<MovieQueryGetAll, ICollection<MovieDTO>>
    {
        public async Task<ICollection<MovieDTO>> Handle(MovieQueryGetAll query, CancellationToken cancellationToken = default)
        {
            ICollection<MovieDTO> movies = await appDbContext.Movies.Select(e => MovieDTO.FromEntity(e)!).ToListAsync(cancellationToken);
            return movies;
        }
    }
}