using HW14.MovieManager.Data.Context;
using HW14.MovieManager.Data.Entities;
using MediatR;

namespace HW14.MovieManager.Service.CommandsAndQueries.Sessions;

public record SessionCommandCreate : IRequest<int>
{
    public int MovieId { get; set; }

    public string RoomName { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<SessionCommandCreate, int>
    {
        public async Task<int> Handle(SessionCommandCreate command, CancellationToken cancellationToken = default)
        {
            Session session = new ()
            {
                MovieId = command.MovieId,
                RoomName = command.RoomName,
                StartAt = command.StartAt,
            };

            await appDbContext.Sessions.AddAsync(session, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return session.Id;
        }
    }
}