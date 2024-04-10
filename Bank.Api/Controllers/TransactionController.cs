using Bank.Core.Interfaces;
using Bank.Domain.DTO.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> CreateTransaction(TransferRequest request)
        {
            var userId = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            var result = await _transactionService.CreateTransfer(request, userId);
            return result ? Ok("Transfer successful") : BadRequest();
        }
    }
}
