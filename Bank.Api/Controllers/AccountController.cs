using Bank.Core.Interfaces;
using Bank.Domain.DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("my-accounts")]
        public async Task<IActionResult> GetAccountsOverview()
        {
            var userId = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            var result = await _accountService.GetAccountsOverview(userId);

            return result != null ? Ok(result) : BadRequest();
            
        }

        [HttpGet("account-details")]
        public async Task<IActionResult> GetAccountDetails()
        {
            var userId = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            if (Request.Headers.TryGetValue("accountId", out StringValues accountIdString))
            {
                if (int.TryParse(accountIdString, out int accountId))
                {
                    var result = await _accountService.GetAccountDetails(accountId, userId);
                    return result != null ? Ok(result) : BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount(NewAccountRequest request)
        {
            var userId = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var result = await _accountService.CreateAccount(request, userId);
            return result != null ? Ok(result) : BadRequest();
        }
    }
}
