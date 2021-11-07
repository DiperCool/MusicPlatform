using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Web.Domain.Entities;
using Web.Infrastructure.Identity;

namespace Web.WebUI.Services
{
    public class IdentityProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Web.Application.Common.Interfaces.IProfileService _profileService;

        public IdentityProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, Web.Application.Common.Interfaces.IProfileService profileService)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _profileService = profileService;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await _userManager.FindByIdAsync(sub);
            ClaimsPrincipal principal = await _claimsFactory.CreateAsync(user);
            Profile profile = await _profileService.GetProfileByUserId(sub);
            List<Claim>  claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            var roles = await _userManager.GetRolesAsync(user);
            
            claims.AddRange(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));
            claims.Add(new Claim(JwtClaimTypes.NickName, profile.Login));
            claims.Add(new Claim(JwtClaimTypes.GivenName, profile.FirstName));
            claims.Add(new Claim(JwtClaimTypes.FamilyName, profile.LastName));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}