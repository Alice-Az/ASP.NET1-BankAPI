using Bank.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            if (result != null)
                return Ok(result);

            return BadRequest();
        }
    }
}
