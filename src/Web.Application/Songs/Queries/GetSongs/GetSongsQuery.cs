using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Common.DTOs;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Mappings;
using Web.Application.Common.Models;

namespace Web.Application.Songs.Queries.GetSongs
{
    public class GetSongsQuery : IRequest<PaginatedList<SongDTO>>
    {
        public int AlbumId { get; set; }
        public int SongId { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class GetSongsQueryHandler : IRequestHandler<GetSongsQuery, PaginatedList<SongDTO>>
    {
        IApplicationDbContext _context;
        IMapper _mapper;

        public GetSongsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SongDTO>> Handle(GetSongsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Songs
                        .Where(x=>x.AlbumId==request.AlbumId)
                        .OrderBy(x=>x.Title)
                        .Include(x=>x.Album)
                            .ThenInclude(x=>x.Picture)
                        .Include(x=>x.Album)
                            .ThenInclude(x=>x.Artist)
                        .ProjectTo<SongDTO>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.SongId, request.PageSize);
        }
    }
}