using HW05.MovieManager.Application.CommandsAndQueries.Movies;
using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Sessions;


public record SessionCommandUpdate : IRequest<bool>
{
    public int MovieId { get; set; }

    public string RoomName { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }
}




public record SessionCommandUpdateSingle : SessionCommandUpdate
{
    public int Id { get; set; }

    public SessionCommandUpdateSingle(int id, SessionCommandUpdate updateCommand)
    {

        Id = id;
        MovieId = updateCommand.MovieId;
        RoomName = updateCommand.RoomName;
        StartAt = updateCommand.StartAt;
    }


    public class Handler(IAppDbContext appDbContext) : IRequestHandler<SessionCommandUpdateSingle, bool>
    {
        public async Task<bool> Handle(SessionCommandUpdateSingle command, CancellationToken cancellationToken = default)
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