using HW14.MovieActors.Data.Context;
using HW14.MovieActors.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieActors.Service.CommandsAndQueries.Actors;

public record ActorCommandDeleteSingle(int Id) : IRequest<bool>
{
    public class Handler(AppDbContext appDbContext) : IRequestHandler<ActorCommandDeleteSingle, bool>
    {
        public async Task<bool> Handle(ActorCommandDeleteSingle command, CancellationToken cancellationToken = default)
        {
            Actor? actor = await appDbContext.Actors.Where(e => e.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if(actor == null) return false;

            appDbContext.Actors.Remove(actor);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}