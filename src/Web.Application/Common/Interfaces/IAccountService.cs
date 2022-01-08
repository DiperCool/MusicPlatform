using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Entities;

namespace Web.Infrastructure.Services;
public interface IAccountService
{
    Task<Account> CreateAccount(Account account);
    Task<bool> IsLoginExist(string login);
    Task<Account> GetAccountByLogin(string login);
}