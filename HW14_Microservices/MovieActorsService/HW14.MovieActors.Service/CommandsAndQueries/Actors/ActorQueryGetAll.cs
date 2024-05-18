using HW14.MovieActors.Contract.DTOs;
using HW14.MovieActors.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieActors.Service.CommandsAndQueries.Actors;

public record ActorQueryGetAll : IRequest<ICollection<ActorDTO>>
{
    public class Handler(AppDbContext appDbContext) : IRequestHandler<ActorQueryGetAll, ICollection<ActorDTO>>
    {
        public async Task<ICollection<ActorDTO>> Handle(ActorQueryGetAll query, CancellationToken cancellationToken = default)
        {
            ICollection<ActorDTO> actors = await appDbContext.Actors
                .OrderByDescending(e => e.Id)
                .Select(e => ActorDTO.FromEntity(e)!)
                .ToListAsync(cancellationToken);
            return actors;
        }
    }
}