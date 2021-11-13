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
using Web.Application.Common.Exceptions;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Mappings;
using Web.Application.Common.Models;
using Web.Application.Common.Security;

namespace Web.Application.Albums.Queries.GetAlbum
{
    [Authorize]
    public class GetAlbumQuery: IRequest<AlbumDTO>
    {
        public int AlbumId { get; set; }
    }
    public class GetAlbumQueryHandler : IRequestHandler<GetAlbumQuery, AlbumDTO>
    {
        IApplicationDbContext _context;
        IMapper _mapper;

        public GetAlbumQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AlbumDTO> Handle(GetAlbumQuery request, CancellationToken cancellationToken)
        {
            return await _context.Albums
                        .Where(x=>x.Id==request.AlbumId)
                        .Include(x=>x.Artist)
                            .ThenInclude(x=>x.Profile)
                        .Include(x=>x.Picture)
                        .ProjectTo<AlbumDTO>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync() ?? throw new NotFoundException("Album not found");
        }
    }
}