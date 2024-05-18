using HW14.MovieManager.Contract.DTOs;
using HW14.MovieManager.Data.Context;
using HW14.MovieManager.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieManager.Service.CommandsAndQueries.Sessions;

public record SessionQueryGetById(int Id) : IRequest<SessionDTO?>
{
    public class Handler(AppDbContext appDbContext) : IRequestHandler<SessionQueryGetById, SessionDTO?>
    {
        public async Task<SessionDTO?> Handle(SessionQueryGetById query, CancellationToken cancellationToken = default)
        {
            Session? session = await appDbContext.Sessions
                .Include(e => e.Movie)
                .Where(e => e.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);
            return SessionDTO.FromEntity(session);
        }
    }
}