using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Application.Common.Interfaces;
using Web.Domain.Entities;
using Web.Infrastructure.Persistence;

namespace Web.Infrastructure.Services;
public class ProfileService : IProfileService
{
    private readonly ApplicationDbContext _context;

    public ProfileService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Profile> UpdateProfile(string userId, Profile profile)
    {
        Account acc = await _context.Accounts.FirstOrDefaultAsync(x=>x.UserId==userId);
        profile.Account = acc;
        
        _context.Profiles.Update(profile);
        await _context.SaveChangesAsync();
        return profile;
    }
    public async Task<Profile> GetProfileByUserId(string userId)
    {
        return await _context.Profiles
                    .Include(x=>x.Picture)
                    .FirstOrDefaultAsync(x=>x.UserId == userId);
    }

    public async Task<Profile> CreateProfile(Profile profile)
    {
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
        return profile;
    }
}