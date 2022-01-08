using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Web.Application.Common.Interfaces;
using Web.Domain.Entities;

namespace Web.WebUI.Services;
public class CurrentUserService:ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    IApplicationDbContext _context;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor, IApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public async Task<Account> GetAccountByUserId()
    {
        return await _context.Accounts.FirstOrDefaultAsync(x=>x.UserId == UserId);
    }

    public async Task<Artist> GetArtistByUserId()
    {
        return await _context.Artists.FirstOrDefaultAsync(x=>x.UserId == UserId);
    }

    public async Task<Listener> GetListenerByUserId()
    {
        return await _context.Listeners.FirstOrDefaultAsync(x=>x.UserId == UserId);
    }
}
