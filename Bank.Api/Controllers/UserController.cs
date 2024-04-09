using Bank.Core.Interfaces;
using Bank.Domain.DTO.Customer;
using Bank.Domain.DTO.Login;
using Bank.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Identity sätts upp med dependency injection
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ICustomerService _customerService;

        public UserController(UserManager<User> userManager,
                              SignInManager<User> signinManager, ITokenService tokenService, ICustomerService customerService)
        {
            _signInManager = signinManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _customerService = customerService;
        }


        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCustomer(CustomerRequest request)
        {
            var user = new User()
            {
                UserName = request.Username,
                Email = request.Emailaddress,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Client");
                int? customerId = await _customerService.CreateCustomer(request, user.Id);

                return customerId.HasValue ? Ok("User created with Id: " + customerId) 
                                           : BadRequest();
            }
            else
            {
                return BadRequest("Error occured");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            //Kontrollerar inloggningen mot databasen och tabellen AspNetUsers
            User? user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var response = _tokenService.GetToken(user, roles);
                    return Ok(response);
                }
                else
                {
                    return Unauthorized();
                }
            }
            return BadRequest();
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(string username, string password)
        {
            var result = await _userManager.CreateAsync(new User() { UserName = username }, password);
            return result.Succeeded ? Ok() : BadRequest("Error occured");
        }
    }
}
