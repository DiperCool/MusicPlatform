using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Security;

namespace Web.Application.Albums.Queries.HasAlbumArtist;
[Authorize(Roles = "Artist")]
public class HasAlbumArtistQuery: IRequest<bool>
{
    public int AlbumId { get; set; }
}

public class HasAlbumArtistQueryHandler : IRequestHandler<HasAlbumArtistQuery, bool>
{
    IApplicationDbContext _context;

    ICurrentUserService _userService;

    public HasAlbumArtistQueryHandler(IApplicationDbContext context, ICurrentUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<bool> Handle(HasAlbumArtistQuery request, CancellationToken cancellationToken)
    {
        return await _context.Albums.AnyAsync(x=>x.Artist.UserId==_userService.UserId&&x.Id==request.AlbumId);
    }
}