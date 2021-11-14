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

namespace Web.Application.Songs.Queries.GetFullPathesSong
{
    public class GetFullPathesSongQuery : IRequest<string>
    {
        public int SongId { get; set; }
    }
    public class GetFullPathesSongQueryHandler : IRequestHandler<GetFullPathesSongQuery, string>
    {
        IApplicationDbContext _context;

        public GetFullPathesSongQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetFullPathesSongQuery request, CancellationToken cancellationToken)
        {
            return (await _context.Songs
                    .Include(x=>x.File)
                    .FirstOrDefaultAsync(x=>x.Id == request.SongId))?.File?.FullPath
                    ?? throw new NotFoundException("Song not found");
        }
    }
}