using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Sessions;

public record SessionQueryGetAll : IRequest<ICollection<SessionDTO>>
{
    public class Handler(IAppDbContext appDbContext) : IRequestHandler<SessionQueryGetAll, ICollection<SessionDTO>>
    {
        public async Task<ICollection<SessionDTO>> Handle(SessionQueryGetAll query, CancellationToken cancellationToken = default)
        {
            ICollection<SessionDTO> sessions = await appDbContext.Sessions.Select(e => SessionDTO.FromEntity(e)!).ToListAsync(cancellationToken);
            return sessions;
        }
    }
}