using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Entities;

namespace Web.Application.Common.Interfaces;
public interface IProfileService
{
    Task<Profile> UpdateProfile(string userId,Profile profile);
    Task<Profile> GetProfileByUserId(string userId);
    Task<Profile> CreateProfile(Profile profile);
}