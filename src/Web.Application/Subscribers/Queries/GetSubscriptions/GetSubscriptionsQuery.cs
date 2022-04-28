using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Web.Application.Common.DTOs;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Mappings;
using Web.Application.Common.Security;

namespace Web.Application.Subscribers.Queries.GetSubscriptions;
[Authorize(Roles="Listener")]
public class GetSubscriptionsQuery: IRequest<PaginatedList<SubscriberDTO>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsQuery, PaginatedList<SubscriberDTO>>
{
    IApplicationDbContext _context;
    ICurrentUserService _currentUser;
    IMapper _mapper;

    public GetSubscriptionsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper)
    {
        _context = context;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SubscriberDTO>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Subscribers
                        .Where(x=>x.Listener.UserId==_currentUser.UserId)
                        .OrderBy(x=>x.SubscribedAt)
                        .ProjectTo<SubscriberDTO>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
                        
    }
}
