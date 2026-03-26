using BankingService.API.Models;
using BankingService.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BankingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /* I have used 
          Custom Exceptions → Service Layer.
          Global Exception Handler → Middleware.
          Controller → No try-catch needed to make controller clean.
        */

        [HttpPost]
        public async Task<IActionResult> CreateAccount(string customerName)
        {
            var result = await _accountService.CreateAccount(customerName);
            return Ok(result);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            var account = await _accountService.GetAccount(accountId);
            return Ok(account);
        }
    }
}
