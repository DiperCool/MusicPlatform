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
using Web.Application.Common.Security;

namespace Web.Application.Likes.Queries.GetLikes;
[Authorize(Roles="Listener")]
public class GetLikesQuery: IRequest<PaginatedList<LikeDTO>>
{
    public int PageSize { get; set; } = 1;
    public int PageNumber { get; set; } = 1;
}
public class GetLikesQueryHandler : IRequestHandler<GetLikesQuery, PaginatedList<LikeDTO>>
{
    IApplicationDbContext _context;
    ICurrentUserService _currentUser;
    IMapper _mapper;

    public GetLikesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper)
    {
        _context = context;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LikeDTO>> Handle(GetLikesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Likes
                .Where(x=>x.Listener.UserId == _currentUser.UserId)
                .OrderBy(x=>x.LikedAt)
                .ProjectTo<LikeDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}