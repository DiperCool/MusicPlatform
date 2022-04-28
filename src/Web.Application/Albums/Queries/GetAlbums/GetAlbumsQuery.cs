using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Common.DTOs;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Mappings;
using Web.Application.Common.Models;
using Web.Application.Common.Security;

namespace Web.Application.Albums.Queries.GetAlbums;
[Authorize]
public class GetAlbumsQuery: IRequest<PaginatedList<AlbumDTO>>
{
    public int ArtistId { get; set; }
    public int PageSize { get; set; } = 1;
    public int PageNumber { get; set; } = 1;
}
public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, PaginatedList<AlbumDTO>>
{
    IApplicationDbContext _context;
    IMapper _mapper;

    public GetAlbumsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AlbumDTO>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Albums
                    .Where(x=>x.Artist.Id==request.ArtistId)
                    .OrderBy(x=>x.CreatedAt)
                    .Include(x=>x.Artist)
                        .ThenInclude(x=>x.Profile)
                    .Include(x=>x.Picture)
                    .ProjectTo<AlbumDTO>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}