using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Common.Exceptions;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Models;
using Web.Application.Common.Security;
using Web.Domain.Entities;

namespace Web.Application.Songs.Commands.CreateSong
{
    [Authorize(Roles="Artist")]
    public class CreateSongCommand: IRequest<int>
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public FileModel File { get; set; }
    }
    public class CreateSongCommandHandler : IRequestHandler<CreateSongCommand, int>
    {
        IApplicationDbContext _context;
        IFileService _fileService;
        ICurrentUserService _currentUserService;

        public CreateSongCommandHandler(IApplicationDbContext context, IFileService fileService, ICurrentUserService currentUserService)
        {
            _context = context;
            _fileService = fileService;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateSongCommand request, CancellationToken cancellationToken)
        {
            if(! await _context.Albums.AnyAsync(x=>x.Id == request.AlbumId && x.Artist.UserId==_currentUserService.UserId))
            {
                throw new ForbiddenAccessException();
            }
            Song song = new Song{ Title = request.Title, AlbumId = request.AlbumId, File = _fileService.SaveFile(request.File), CreatedAt =  DateTime.Now.ToUniversalTime() };
            _context.Songs.Add(song);
            await _context.SaveChangesAsync(cancellationToken);
            return song.Id;
        }
    }
}