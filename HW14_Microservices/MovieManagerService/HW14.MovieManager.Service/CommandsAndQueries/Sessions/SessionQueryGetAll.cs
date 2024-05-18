using HW14.MovieManager.Contract.DTOs;
using HW14.MovieManager.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieManager.Service.CommandsAndQueries.Sessions;

public record SessionQueryGetAll : IRequest<ICollection<SessionDTO>>
{
    public class Handler(AppDbContext appDbContext) : IRequestHandler<SessionQueryGetAll, ICollection<SessionDTO>>
    {
        public async Task<ICollection<SessionDTO>> Handle(SessionQueryGetAll query, CancellationToken cancellationToken = default)
        {
            ICollection<SessionDTO> sessions = await appDbContext.Sessions
                //.Include(e => e.Movie)
                .OrderByDescending(e => e.Id)
                .Select(e => SessionDTO.FromEntity(e)!)
                .ToListAsync(cancellationToken);
            return sessions;
        }
    }
}