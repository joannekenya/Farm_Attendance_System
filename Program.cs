using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FARM_ATTENDANCE_SYSTEM.Data;
using FARM_ATTENDANCE_SYSTEM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add Database Context
builder.Services.AddDbContext<FarmDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dev_Connection")));


// 🔹 Register Services
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();  // Already included by AddIdentity
builder.Services.AddScoped<TokenProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 🔹 Configure Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 🔹 Configure JWT Authentication
var key = Encoding.UTF8.GetBytes("YourSecureSecretKey12345678901234567890"); // Ensure this is 32+ chars
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// 🔹 Register Syncfusion License (if needed)
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YourLicenseKeyHere");

// 🔹 Build App
var app = builder.Build();
app.UseWebSockets();

// 🔹 Configure Middleware Order
app.UseStaticFiles();
app.UseSession();
app.UseRouting();  // Ensure this is before Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
