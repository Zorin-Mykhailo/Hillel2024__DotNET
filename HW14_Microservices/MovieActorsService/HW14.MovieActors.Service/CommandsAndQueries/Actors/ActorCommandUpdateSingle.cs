using HW14.MovieActors.Data.Context;
using HW14.MovieActors.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieActors.Service.CommandsAndQueries.Actors;

public record ActorCommandUpdate : IRequest<bool>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public string Description { get; set; } = string.Empty;
}



public record ActorCommandUpdateSingle : ActorCommandUpdate
{
    public int Id { get; set; }

    public ActorCommandUpdateSingle(int id, ActorCommandUpdate updateComand)
    {
        Id = id;
        FirstName = updateComand.FirstName;
        LastName = updateComand.LastName;
        DateOfBirth = updateComand.DateOfBirth;
        Description = updateComand.Description;
    }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<ActorCommandUpdateSingle, bool>
    {
        public async Task<bool> Handle(ActorCommandUpdateSingle command, CancellationToken cancellationToken = default)
        {
            Actor? actor = await appDbContext.Actors.Where(e => e.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if(actor == null) return false;

            actor.FirstName = command.FirstName;
            actor.LastName = command.LastName;
            actor.DateOfBirth = command.DateOfBirth;
            actor.Description = command.Description;

            await appDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}