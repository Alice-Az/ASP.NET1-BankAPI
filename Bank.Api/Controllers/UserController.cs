using Bank.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Identity sätts upp med dependency injection
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager,
                              SignInManager<User> signinManager)
        {
            _signInManager = signinManager;
            _userManager = userManager;
        }


        [HttpPost]
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
        public async Task<IActionResult> Login(string username, string password)
        {
            //Kontrollerar inloggningen mot databasen och tabellen AspNetUsers
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);


            if (result.Succeeded)
            {
                //Här kan vi lägga på en JWT token och skicka med i responsen

                return Ok("Logged in");
            }
            else
            {
                return Unauthorized();
            }
        }


    }
}
