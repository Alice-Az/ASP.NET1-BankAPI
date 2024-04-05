using Bank.Data;
using Bank.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<BankAppDataContext>(options =>
    options.UseSqlServer("Data Source=localhost;Initial Catalog=BankAppData;Integrated Security=SSPI;TrustServerCertificate=True;"));

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BankAppDataContext>()
                .AddDefaultTokenProviders();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
