using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.DTOs;
using HW05.MovieManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Sessions;

public record SessionQueryGetById(int Id) : IRequest<SessionDTO?>
{
    public class Handler(IAppDbContext appDbContext) : IRequestHandler<SessionQueryGetById, SessionDTO?>
    {
        public async Task<SessionDTO?> Handle(SessionQueryGetById query, CancellationToken cancellationToken = default)
        {
            Session? session = await appDbContext.Sessions.Where(e => e.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            return SessionDTO.FromEntity(session);
        }
    }
}