using BankingService.API.Constants;
using BankingService.API.DTOs;
using BankingService.API.Exceptions;
using BankingService.API.Models;
using BankingService.API.Repositories;
using Microsoft.Identity.Client;

namespace BankingService.API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> Deposit(int accountId, Decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }

            if (amount > TransactionLimits.MaxDeposit)
            {
                throw new DepositLimitExceededException(TransactionLimits.MaxDeposit, amount);
            }

            var account = await _transactionRepository.GetAccountAsync(accountId);

            if (account == null)
            {
                throw new AccountNotFoundException(accountId);
            }

            //Update balance
            account.Balance += amount;

            await _transactionRepository.UpdateAccountAsync(account);

            // Create transaction record
            var transaction = new Transaction
            {
                 AccountId = accountId,
                 Amount = amount,
                 TransactionType = "Deposit",
                 TransactionDate = DateTime.UtcNow

            };

            var result = await _transactionRepository.DepositAmountAsync(transaction);
            return result;
        }

        public async Task<List<AccountTransactionDto>> GetTransactionHistory(int accountID)
        {
            var account = await _transactionRepository.GetAccountAsync(accountID);

            if (account == null)
            {
                throw new AccountNotFoundException(accountID);
            }

            var transactions = await _transactionRepository.GetTransactionByAccountAsync(accountID);

            if (!transactions.Any())
            {
                throw new TransactionsNotFound(accountID);
            }

            decimal runningBalance = 0;
            var transactionDTO = new List<AccountTransactionDto>();

            foreach (var t in transactions) 
            { 
                if(t.TransactionType.Equals("Deposit", StringComparison.OrdinalIgnoreCase))
                    runningBalance += t.Amount;
                else if(t.TransactionType.Equals("Withdraw", StringComparison.OrdinalIgnoreCase))
                    runningBalance -= t.Amount;

                transactionDTO.Add(new AccountTransactionDto
                {
                    TransactionID = t.TransactionId,
                    AccountID = t.AccountId,
                    Amount = t.Amount,
                    TransactionType = t.TransactionType,
                    TransactionDate = DateTime.UtcNow,
                    Balance = runningBalance
                });    
            }
            var result = transactionDTO.OrderByDescending(t => t.TransactionDate).ToList();
            return result;
        }

        public async Task<Transaction> Withdraw(int accountID, Decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive.");
            }

            if (amount > TransactionLimits.MaxWithdrawal)
            {
                throw new WithrawLimitExceededException(TransactionLimits.MaxWithdrawal, amount);
            }

            var account = await _transactionRepository.GetAccountAsync(accountID);

            if (account == null)
            {
                throw new AccountNotFoundException(accountID);
            }

            if (account.Balance < amount)
            {
                throw new InsufficientBalanceException();
            }

            //Update Balance
            account.Balance -= amount;

            await _transactionRepository.UpdateAccountAsync(account);

            //Create Trancation Record

            var transaction = new Transaction
            {
                AccountId = accountID,
                Amount = amount,
                TransactionType = "Withdraw",
                TransactionDate = DateTime.UtcNow
            };

            var result = await _transactionRepository.DepositAmountAsync(transaction);
            return result;
        }
    }
}
