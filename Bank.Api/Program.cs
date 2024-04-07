using Bank.Core.Interfaces;
using Bank.Core.Services;
using Bank.Data;
using Bank.Domain.DTO.Options;
using Bank.Domain.Identity;
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

builder.Services.AddScoped<ITokenService, TokenService>();

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
