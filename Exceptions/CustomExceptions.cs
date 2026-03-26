namespace BankingService.API.Exceptions
{
    public class AccountNotFoundException : BaseException
    {
        public AccountNotFoundException(int accountID) : base($"Account with ID {accountID} not found.", 404)
        {

        }
    }
    public class InsufficientBalanceException : BaseException
    {
        public InsufficientBalanceException() : base("Insufficient balance in the account.", 400)
        {

        }
    }
    public class TransactionsNotFound : BaseException
    {
        public TransactionsNotFound(int accountID) : base($"No transactions found for the Account with ID {accountID}.", 404)
        {

        }
    }
    public class DepositLimitExceededException : BaseException
    {
        public decimal MaxLimit { get; }
        public decimal AttemptedAmount { get;}

        public DepositLimitExceededException (decimal maxLimit, decimal attemptedAmount) : base ($"Deposit limit exceeded. Maximum allowed is {maxLimit}, attempted {attemptedAmount}.", 422)
        {
            MaxLimit = maxLimit;
            AttemptedAmount = attemptedAmount;
        }
    }

    public class WithrawLimitExceededException : BaseException
    {
        public decimal MaxLimit { get; }
        public decimal AttemptedAmount { get; }

        public WithrawLimitExceededException(decimal maxLimit, decimal attemptedAmount) : base($"Withdrawal limit exceeded. Maximum allowed is {maxLimit}, attempted {attemptedAmount}.", 422)
        {
            MaxLimit = maxLimit;
            AttemptedAmount = attemptedAmount;
        }
    }
}
