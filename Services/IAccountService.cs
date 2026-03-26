using BankingService.API.Models;

namespace BankingService.API.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccount(string customerName);
        Task<Account> GetAccount(int accountId);
    }
}
