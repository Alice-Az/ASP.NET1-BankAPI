using Bank.Core.Interfaces;
using Bank.Data;
using Bank.Domain.DTO.Login;
using Bank.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Identity sätts upp med dependency injection
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly BankAppDataContext _context;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<User> userManager,
                              SignInManager<User> signinManager, BankAppDataContext context, ITokenService tokenService)
        {
            _signInManager = signinManager;
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(string username)
        {

            var userModel = new User();
            userModel.UserName = username;

            var result = await _userManager.CreateAsync(userModel, "1Z2fwelfjn#lkg3!");

            if (result.Succeeded)
            {
                return Ok("User created");
            }
            else
            {
                return BadRequest("Error occured");
            }
        }

        [HttpPost]
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

        
    }
}
