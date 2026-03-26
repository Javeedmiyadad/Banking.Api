using BankingService.API.Data;
using BankingService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingService.API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _bankingDbContext;

        public AccountRepository(BankingDbContext bankingDbContext)
        {
            _bankingDbContext = bankingDbContext;
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            _bankingDbContext.Accounts.Add(account);
            await _bankingDbContext.SaveChangesAsync();

            return account;
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _bankingDbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
        }
    }
}
