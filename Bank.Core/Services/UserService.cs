using Bank.Core.Interfaces;
using Bank.Domain.DTO.Customer;
using Bank.Domain.DTO.Login;
using Bank.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Bank.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ICustomerService _customerService;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _customerService = customerService;
        }

        public async Task<int?> CreateUser(CustomerRequest request)
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
                return customerId.HasValue ? customerId : null;
            };
            return null;
        }

        public async Task<string?> Login(LoginRequest request)
        {
            User? user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return _tokenService.GetToken(user, roles);
                }
            }
            return null;
        }
    }
}
