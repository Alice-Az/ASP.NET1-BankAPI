using Bank.Core.Interfaces;
using Bank.Domain.DTO.Loan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan(LoanRequest request)
        {
            bool loanCreated = await _loanService.CreateLoan(request);
            if (loanCreated) { return Ok("Loan created"); }

            return BadRequest();
        }
    }
}
