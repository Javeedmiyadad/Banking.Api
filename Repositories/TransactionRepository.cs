using BankingService.API.Data;
using BankingService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingService.API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingDbContext _bankingDbContext;

        public TransactionRepository(BankingDbContext bankingDbContext)
        {
            _bankingDbContext = bankingDbContext;
        }
        public async Task<Transaction> DepositAmountAsync(Transaction transaction)
        {
            await _bankingDbContext.Transactions.AddAsync(transaction);
            await _bankingDbContext.SaveChangesAsync();

            return transaction;
        }

        public async Task<Account> GetAccountAsync(int accountId)
        {
            return await _bankingDbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task<List<Transaction>> GetTransactionByAccountAsync(int accountId)
        {
            return await _bankingDbContext.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderBy(t => t.TransactionDate)
                .ToListAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _bankingDbContext.Accounts.Update(account);
            await _bankingDbContext.SaveChangesAsync();
        }
    }
}
