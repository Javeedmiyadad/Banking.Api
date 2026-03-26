using BankingService.API.Models;

namespace BankingService.API.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> GetAccountByIdAsync(int accountId);
       
    }
}
