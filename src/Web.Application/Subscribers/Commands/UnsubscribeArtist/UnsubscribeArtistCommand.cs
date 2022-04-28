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

namespace Web.Application.Subscribers.Commands.UnsubscribeArtist;
[Authorize(Roles="Listener")]
public class UnsubscribeArtistCommand: IRequest
{
    public int ArtistId { get; set;}
}
public class UnsubscribeArtistCommandHandler : IRequestHandler<UnsubscribeArtistCommand, Unit>
{
    IApplicationDbContext _context;
    ICurrentUserService _userCurrent;

    public UnsubscribeArtistCommandHandler(IApplicationDbContext context, ICurrentUserService userCurrent)
    {
        _context = context;
        _userCurrent = userCurrent;
    }

    public async Task<Unit> Handle(UnsubscribeArtistCommand request, CancellationToken cancellationToken)
    {
        Subscriber subscriber = await _context.Subscribers.FirstOrDefaultAsync(x=>x.ArtistId==request.ArtistId&&x.Listener.UserId==_userCurrent.UserId) ?? throw new ForbiddenAccessException();
        Artist artist = await _context.Artists.FirstOrDefaultAsync(x=>x.Id==request.ArtistId);
        artist.Subscribers-=1;
        _context.Subscribers.Remove(subscriber);
        _context.Artists.Update(artist);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
