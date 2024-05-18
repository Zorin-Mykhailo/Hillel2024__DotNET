using HW14.MovieActors.Data.Context;
using HW14.MovieActors.Data.Entities;
using MediatR;

namespace HW14.MovieActors.Service.CommandsAndQueries.Actors;

public record ActorCommandCreate : IRequest<int>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public string Description { get; set; } = string.Empty;

    public class Handler(AppDbContext appDbContext) : IRequestHandler<ActorCommandCreate, int>
    {
        public async Task<int> Handle(ActorCommandCreate command, CancellationToken cancellationToken = default)
        {
            Actor actor = new()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                DateOfBirth = command.DateOfBirth,
                Description = command.Description,
            };

            await appDbContext.Actors.AddAsync(actor, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return actor.Id;
        }
    }
}