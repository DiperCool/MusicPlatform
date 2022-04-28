using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Web.Application.Common.Exceptions;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Security;
using Web.Domain.Entities;

namespace Web.Application.Likes.Commands.LikeSong;
[Authorize(Roles="Listener")]
public class LikeSongCommand: IRequest<int>
{
    public int SongId { get; set; }
}

public class LikeSongCommandHandler : IRequestHandler<LikeSongCommand, int>
{
    IApplicationDbContext _context;
    ICurrentUserService _currentUser;

    public LikeSongCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(LikeSongCommand request, CancellationToken cancellationToken)
    {
        Listener listener = await _currentUser.GetListenerByUserId();
        if(_context.Likes.Any(x=>x.Listener.UserId==listener.UserId && x.SongId==request.SongId ))
        {
            throw new ForbiddenAccessException();
        }
        Like like = new(){ Listener = listener, SongId=request.SongId, LikedAt=DateTime.Now.ToUniversalTime() };
        await _context.Likes.AddAsync(like);
        await _context.SaveChangesAsync(cancellationToken);
        return like.Id;
    }
}
