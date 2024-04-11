using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Sessions;

public class SessionCommandUpdateById : IRequest<bool>
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public string RoomName { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }

    public class Handler(IAppDbContext appDbContext) : IRequestHandler<SessionCommandUpdateById, bool>
    {
        public async Task<bool> Handle(SessionCommandUpdateById command, CancellationToken cancellationToken = default)
        {
            Session? session = await appDbContext.Sessions.Where(e => e.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if(session == null) return false;

            session.MovieId = command.MovieId;
            session.RoomName = command.RoomName;
            session.StartAt = command.StartAt;

            await appDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}