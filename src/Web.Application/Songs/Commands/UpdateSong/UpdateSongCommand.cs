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

namespace Web.Application.Songs.Commands.UpdateSong;
[Authorize(Roles="Artist")]
public class UpdateSongCommand: IRequest<UpdateSongResult>
{
    public int SongId { get; set; }
    public string Title { get; set; }
    public FileModel File { get; set; }
}
public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommand, UpdateSongResult>
{
    IApplicationDbContext _context;
    IFileService _fileService;
    ICurrentUserService _currentUserService;

    public UpdateSongCommandHandler(IApplicationDbContext context, IFileService fileService, ICurrentUserService currentUserService)
    {
        _context = context;
        _fileService = fileService;
        _currentUserService = currentUserService;
    }

    public async Task<UpdateSongResult> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        Song song = await _context.Songs
                    .Where(x=>x.Id== request.SongId && x.Album.Artist.UserId==_currentUserService.UserId)
                    .Include(x=>x.Album)
                        .ThenInclude(x=>x.Artist)
                    .FirstOrDefaultAsync();
        if(song ==null)
        {
            throw new ForbiddenAccessException();
        }
        if(request.File != null)
        {
            _fileService.DeleteFile(song.File.FullPath);
            PathToFile pathToFile = _fileService.SaveFile(request.File);
            song.File.FullPath = pathToFile.FullPath;
            song.File.ShortPath= pathToFile.ShortPath;
            _context.PathesToFiles.Update(song.File);
        }
        if(!string.IsNullOrEmpty(request.Title))
        {
            song.Title= request.Title;
            _context.Songs.Update(song);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateSongResult{
            Title = song.Title
        };

    }
}