using BankingService.API.Data;
using BankingService.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BankingService.API.Controllers
{
    [ApiController]
    [Route("odata/transactions")]
    public class TransactionODataController : ControllerBase
    {
       private readonly BankingDbContext _bankingDbContext;

        public TransactionODataController(BankingDbContext bankingDbContext) 
        {
            _bankingDbContext = bankingDbContext;
        }

        /* I have used 
          Custom Exceptions → Service Layer.
          Global Exception Handler → Middleware.
          Controller → No try-catch needed to make controller clean.
        */

        [EnableQuery]
        [HttpGet]
        public IQueryable<Transaction> Get()
        { 
            var result = _bankingDbContext.Transactions;
            return result;
        }
    }
}
