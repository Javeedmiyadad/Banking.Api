using BankingService.API.DTOs;
using BankingService.API.Models;

namespace BankingService.API.Repositories
{
    public interface ITransactionRepository
    {
        Task<Account> GetAccountAsync(int accountId);
        Task<Transaction> DepositAmountAsync(Transaction transaction);
        Task UpdateAccountAsync(Account account);
        Task<List<Transaction>> GetTransactionByAccountAsync(int accountId);

    }
}
