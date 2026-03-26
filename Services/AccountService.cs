using BankingService.API.Exceptions;
using BankingService.API.Models;
using BankingService.API.Repositories;

namespace BankingService.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService (IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        
        public async Task<Account> CreateAccount(string customerName)
        {
            var account = new Account
            {
                CustomerName = customerName,
                Balance = 0,
                CreatedDate = DateTime.UtcNow
            };

            var result = await _accountRepository.CreateAccountAsync(account);
            return result;
            
        }

        public async Task<Account> GetAccount(int accountId)
        {
            var result = await _accountRepository.GetAccountByIdAsync(accountId);

            if (result == null)
            {
                throw new AccountNotFoundException(accountId);
            }
            return result;
        }
    }
}
