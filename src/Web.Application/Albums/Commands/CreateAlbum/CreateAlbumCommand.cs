using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Web.Application.Common.DTOs;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Models;
using Web.Application.Common.Security;
using Web.Domain.Entities;

namespace Web.Application.Albums.Commands.CreateAlbum
{
    [Authorize(Roles = "Artist")]
    public class CreateAlbumCommand : IRequest<AlbumDTO>
    {
        public string Title { get; set; }
        public FileModel File { get; set; }

        public class CreateAlbumCommandHandle : IRequestHandler<CreateAlbumCommand, AlbumDTO>
        {
            IApplicationDbContext _context;
            ICurrentUserService _currentUser;
            IFileService _fileService;
            IMapper _mapper;

            public CreateAlbumCommandHandle(IApplicationDbContext context, ICurrentUserService currentUser, IFileService fileService, IMapper mapper)
            {
                _context = context;
                _currentUser = currentUser;
                _fileService = fileService;
                _mapper = mapper;
            }

            public async Task<AlbumDTO> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
            {
                Album album = new Album { Title = request.Title };
                Artist artist = await _currentUser.GetArtistByUserId();
                album.Artist = artist;
                album.CreatedAt=  DateTime.Now.ToUniversalTime();
                album.Picture=_fileService.SaveFile(request.File);
                _context.Albums.Add(album);
                await _context.SaveChangesAsync(cancellationToken);
                return _mapper.Map<AlbumDTO>(album);
            }
        }
    }
}