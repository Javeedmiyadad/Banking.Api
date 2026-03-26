using BankingService.API.DTOs;
using BankingService.API.Models;

namespace BankingService.API.Services
{
    public interface ITransactionService
    {
        Task<Transaction> Deposit(int accountID, Decimal amount);
        Task<Transaction> Withdraw(int accountID, Decimal amount);
        Task<List<AccountTransactionDto>> GetTransactionHistory(int accountId);
    }
}
