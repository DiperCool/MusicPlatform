using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Domain.Entities;
using Web.Infrastructure.Persistence;

namespace Web.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        public AccountService(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task<int> CreateAccount(Account account)
        {
            if(account is Artist)
            {
                _context.Artists.Add((Artist)account);
            }
            else if(account is  Listener)
            {
                _context.Listeners.Add((Listener)account);
            }
            else
            {
                _context.Accounts.Add(account);
            }
            await _context.SaveChangesAsync();
            return account.Id;

        }

        public async Task<Account> GetAccountByEmailOrLogin(string loginOrEmail)
        {
            return await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x=>x.Login== loginOrEmail || x.Email==loginOrEmail);
        }

        public async Task<bool> IsLoginExist(string login)
        {
            return await _context.Accounts.AsNoTracking().AnyAsync(x=>x.Login==login);
        }

    }
}