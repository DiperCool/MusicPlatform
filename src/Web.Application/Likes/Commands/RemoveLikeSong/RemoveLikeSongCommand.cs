using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Common.Exceptions;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Security;
using Web.Domain.Entities;

namespace Web.Application.Likes.Commands.RemoveLikeSong;
[Authorize(Roles="Listener")]
public class RemoveLikeSongCommand:IRequest
{
    public int SongId { get; set; }
}

public class RemoveLikeSongCommandHandler : IRequestHandler<RemoveLikeSongCommand, Unit>
{
    IApplicationDbContext _context;
    ICurrentUserService _currentUser;

    public RemoveLikeSongCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(RemoveLikeSongCommand request, CancellationToken cancellationToken)
    {
        Like like = await _context.Likes.FirstOrDefaultAsync(x=>x.Listener.UserId==_currentUser.UserId && x.SongId==request.SongId )??throw new ForbiddenAccessException();
        _context.Likes.Remove(like);


        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}