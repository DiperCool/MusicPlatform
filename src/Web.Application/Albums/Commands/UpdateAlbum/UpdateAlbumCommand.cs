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

namespace Web.Application.Albums.Commands.UpdatePicturesAlbum
{
    [Authorize(Roles ="Artist")]
    public class UpdateAlbumCommand : IRequest<UpdateAlbumResult>
    {
        public FileModel File { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
    }

    public class UpdateAlbumCommandHandle : IRequestHandler<UpdateAlbumCommand, UpdateAlbumResult>
    {
        IFileService _fileService;
        IApplicationDbContext _context;

        ICurrentUserService _userService;

        public UpdateAlbumCommandHandle(IFileService fileService, IApplicationDbContext context, ICurrentUserService userService)
        {
            _fileService = fileService;
            _context = context;
            _userService = userService;
        }

        public async Task<UpdateAlbumResult> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            Album album = await _context.Albums
                                .Include(x=>x.Picture)
                                .FirstOrDefaultAsync(x=>x.Artist.UserId==_userService.UserId && x.Id == request.AlbumId);
            if(album== null)
            {
                throw new NotFoundException("Album not found");
            }
            if(request.File != null)
            {
                _fileService.DeleteFile(album.Picture.FullPath);
                PathToFile pathToFile = _fileService.SaveFile(request.File);
                album.Picture.FullPath = pathToFile.FullPath;
                album.Picture.ShortPath= pathToFile.ShortPath;
                _context.PathesToFiles.Update(album.Picture);
                
            }
            if(!string.IsNullOrEmpty(request.Title))
            {
                album.Title= request.Title;
                _context.Albums.Update(album);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateAlbumResult{
                Title = album.Title,
                ShortPath = album.Picture.ShortPath
            };
        }
    }
}