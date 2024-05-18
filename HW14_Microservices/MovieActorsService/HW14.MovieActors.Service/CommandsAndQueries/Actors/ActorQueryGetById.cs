using HW14.MovieActors.Contract.DTOs;
using HW14.MovieActors.Data.Context;
using HW14.MovieActors.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW14.MovieActors.Service.CommandsAndQueries.Actors;

public record ActorQueryGetById(int Id) : IRequest<ActorDTO?>
{
    public class Handler(AppDbContext appDbContext) : IRequestHandler<ActorQueryGetById, ActorDTO?>
    {
        public async Task<ActorDTO?> Handle(ActorQueryGetById query, CancellationToken cancellationToken = default)
        {
            Actor? actor = await appDbContext.Actors.Where(e => e.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            return ActorDTO.FromEntity(actor);
        }
    }
}