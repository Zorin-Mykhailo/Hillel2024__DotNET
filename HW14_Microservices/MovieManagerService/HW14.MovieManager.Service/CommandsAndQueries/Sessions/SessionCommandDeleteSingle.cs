using HW14.MovieManager.Data.Context;
using HW14.MovieManager.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieManager.Service.CommandsAndQueries.Sessions;

public record SessionCommandDeleteSingle(int Id) : IRequest<bool>
{
    public class Handler(AppDbContext appDbContext) : IRequestHandler<SessionCommandDeleteSingle, bool>
    {
        public async Task<bool> Handle(SessionCommandDeleteSingle command, CancellationToken cancellationToken = default)
        {
            Session? session = await appDbContext.Sessions.Where(e => e.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if(session == null) return false;

            appDbContext.Sessions.Remove(session);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}