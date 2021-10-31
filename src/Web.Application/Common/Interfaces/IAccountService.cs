using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Entities;

namespace Web.Infrastructure.Services
{
    public interface IAccountService
    {
        Task<int> CreateAccount(Account account);
        Task<bool> IsLoginExist(string login);
        Task<Account> GetAccountByEmailOrLogin(string loginOrEmail);
    }
}