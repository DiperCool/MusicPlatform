using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Entities;

namespace Web.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        Task<Artist> GetArtistByUserId();
        Task<Listener> GetListenerByUserId();
        Task<Account> GetAccountByUserId();
    }
}