using BankingService.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
       private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /* I have used 
          Custom Exceptions → Service Layer.
          Global Exception Handler → Middleware.
          Controller → No try-catch needed to make controller clean.
        */

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(int accountId, Decimal amount)
        {

            var result = await _transactionService.Deposit(accountId, amount);
            return Ok(result);
        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw(int accountId, Decimal amount)
        {
            var result = await _transactionService.Withdraw(accountId, amount);
            return Ok(result);
        }

        [HttpGet("History/{accountID}")]
        public async Task<IActionResult> GetTransaction(int accountID)
        {
            var result = await _transactionService.GetTransactionHistory(accountID);
            return Ok(result);
        }
    }
}
