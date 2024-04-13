using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Sessions;

public record SessionCommandDeleteById(int Id) : IRequest<bool>
{
    public class Handler(IAppDbContext appDbContext) : IRequestHandler<SessionCommandDeleteById, bool>
    {
        public async Task<bool> Handle(SessionCommandDeleteById command, CancellationToken cancellationToken = default)
        {
            Session? session = await appDbContext.Sessions.Where(e => e.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if(session == null) return false;

            appDbContext.Sessions.Remove(session);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
