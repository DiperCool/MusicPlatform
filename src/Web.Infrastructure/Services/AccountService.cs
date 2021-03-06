using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Domain.Entities;
using Web.Infrastructure.Persistence;

namespace Web.Infrastructure.Services;
public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _context;
    public AccountService(ApplicationDbContext context)
    {
        _context=context;
    }
    public async Task<Account> CreateAccount(Account account)
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
        return account;

    }

    public async Task<Account> GetAccountByLogin(string login)
    {
        return await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x=>x.Profile.Login== login);
    }

    public async Task<bool> IsLoginExist(string login)
    {
        return await _context.Accounts.AsNoTracking().AnyAsync(x=>x.Profile.Login==login);
    }
}