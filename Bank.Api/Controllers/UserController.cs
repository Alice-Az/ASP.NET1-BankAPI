using Bank.Core.Interfaces;
using Bank.Domain.DTO.Customer;
using Bank.Domain.DTO.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCustomer(CustomerRequest request)
        {
            int? customerId = await _userService.CreateUser(request);
            return customerId.HasValue ? Ok("User created with Id: " + customerId) : BadRequest();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _userService.Login(request);
            return result != null ? Ok(result) : BadRequest();
        }
    }
}
