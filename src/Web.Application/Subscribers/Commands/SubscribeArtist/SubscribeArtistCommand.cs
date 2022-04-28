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

namespace Web.Application.Subscribers.Commands.SubscribeArtist;
[Authorize(Roles="Listener")]
public class SubscribeArtistCommand : IRequest<int>
{
    public int ArtistId { get; set; }
}


public class SubscribeArtistCommandHandler : IRequestHandler<SubscribeArtistCommand, int>
{
    IApplicationDbContext _context;
    ICurrentUserService _userCurrent;

    public SubscribeArtistCommandHandler(IApplicationDbContext context, ICurrentUserService userCurrent)
    {
        _context = context;
        _userCurrent = userCurrent;
    }

    public async Task<int> Handle(SubscribeArtistCommand request, CancellationToken cancellationToken)
    {
        Listener listener = await _userCurrent.GetListenerByUserId();
        if(await _context.Subscribers.AnyAsync(x=>x.Artist.Id==request.ArtistId&&x.Listener.UserId==listener.UserId))
        {
            throw new ForbiddenAccessException();
        }
        Subscriber subscriber = new() { Listener = listener, ArtistId=request.ArtistId, SubscribedAt=DateTime.Now.ToUniversalTime() };
        Artist artist = await _context.Artists.FirstOrDefaultAsync(x=>x.Id==request.ArtistId);
        artist.Subscribers+=1;
        await _context.Subscribers.AddAsync(subscriber);
        _context.Artists.Update(artist);
        await _context.SaveChangesAsync(cancellationToken);
        return subscriber.Id;
    }
}
