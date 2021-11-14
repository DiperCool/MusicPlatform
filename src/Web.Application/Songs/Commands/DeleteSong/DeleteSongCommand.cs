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

namespace Web.Application.Songs.Commands.DeleteSong
{
    [Authorize(Roles="Artist")]
    public class DeleteSongCommand : IRequest
    {
        public int SongId { get; set; }
    }

    public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand>
    {
        IApplicationDbContext _context;
        ICurrentUserService _currentUserService;

        public DeleteSongCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteSongCommand request, CancellationToken cancellationToken)
        {
            Song song = await _context.Songs
                        .FirstOrDefaultAsync(x=>x.Id == request.SongId && x.Album.Artist.UserId==_currentUserService.UserId);
            if(song ==null)
            {
                throw new ForbiddenAccessException();
            }
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}