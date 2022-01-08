using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Common.Exceptions;
using Web.Application.Common.Interfaces;
using Web.Domain.Entities;

namespace Web.Application.Albums.Commands.DeleteAlbum;
public class DeleteAlbumCommand : IRequest
{
    public int AlbumId { get; set; }
}

public class DeleteAlbumCommandHandle : IRequestHandler<DeleteAlbumCommand>
{
    IApplicationDbContext _context;
    ICurrentUserService _currentUser;

    public DeleteAlbumCommandHandle(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
    {
        Album album = await _context.Albums.FirstOrDefaultAsync(x=>x.Artist.UserId == _currentUser.UserId && x.Id==request.AlbumId);
        if(album== null)
        {
            throw new NotFoundException("Album not found");
        }
        _context.Albums.Remove(album);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}