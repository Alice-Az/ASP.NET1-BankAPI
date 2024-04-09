using Bank.Core.Interfaces;
using Bank.Core.Services;
using Bank.Data;
using Bank.Data.Interfaces;
using Bank.Data.Repositories;
using Bank.Domain.DTO.Options;
using Bank.Domain.Identity;
using Bank.Domain.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BankAppDataContext>(options =>
    options.UseSqlServer("Data Source=localhost;Initial Catalog=BankAppData;Integrated Security=SSPI;TrustServerCertificate=True;"));

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BankAppDataContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
});

builder.Services.AddAutoMapper(typeof(CustomerProfile).Assembly);

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanRepo, LoanRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountService, AccountService>();

SecurityOptions security = builder.Configuration.GetSection("Security").Get<SecurityOptions>() ?? new();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt => {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = security.Issuer,
            ValidAudience = security.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(security.Key)),
            RoleClaimType = ClaimTypes.Role
        };
    });

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
