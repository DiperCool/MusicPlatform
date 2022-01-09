using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Web.Application.Common.DTOs;
using Web.Application.Common.Enums;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Mappings;
using Web.Application.Common.Models;

namespace Web.Application.Search.Queries.Search;
public class SearchQuery:IRequest<PaginatedList<SearchDTO>>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageSize { get; set; } = 10;
}

public class SearchQueryHandler : IRequestHandler<SearchQuery, PaginatedList<SearchDTO>>
{
    IApplicationDbContext _context;
    public SearchQueryHandler(IApplicationDbContext context)
    {
        _context=context;
    }
    public async Task<PaginatedList<SearchDTO>> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        IQueryable<SearchDTO> query = _context.Artists.Select(x=> new SearchDTO(){ Id = x.Id, Title = x.Profile.Login, Type= TypeItemSearch.Artist,Picture=x.Profile.Picture.ShortPath})
                    .Concat(_context.Listeners.Select(x=> new SearchDTO(){ Id = x.Id, Title = x.Profile.Login, Type= TypeItemSearch.Listener,Picture=x.Profile.Picture.ShortPath}))
                    .Concat(_context.Songs.Select(x=>new SearchDTO(){ Id=x.Id, Title= x.Title, Type= TypeItemSearch.Song, Picture = x.Album.Picture.ShortPath}))
                    .Concat(_context.Albums.Select(x=> new SearchDTO(){Id=x.Id, Title= x.Title, Type= TypeItemSearch.Album, Picture = x.Picture.ShortPath}));        
        return await query
                .Where(x=>x.Title.ToLower().Contains(request.Title))
                .OrderBy(x=>x.Id)
                .PaginatedListAsync<SearchDTO>(request.Id, request.PageSize);

                

    }
}
