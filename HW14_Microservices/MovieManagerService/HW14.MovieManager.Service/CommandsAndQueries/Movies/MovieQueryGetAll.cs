using HW14.MovieManager.Contract.DTOs;
using HW14.MovieManager.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieManager.Service.CommandsAndQueries.Movies;

public record MovieQueryGetAll : IRequest<ICollection<MovieDTO>>
{
    public class Handler(AppDbContext appDbContext) : IRequestHandler<MovieQueryGetAll, ICollection<MovieDTO>>
    {
        public async Task<ICollection<MovieDTO>> Handle(MovieQueryGetAll query, CancellationToken cancellationToken = default)
        {
            ICollection<MovieDTO> movies = await appDbContext.Movies
                .OrderByDescending(e => e.Id)
                .Select(e => MovieDTO.FromEntity(e)!)
                .ToListAsync(cancellationToken);
            return movies;
        }
    }
}