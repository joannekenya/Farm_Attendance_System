using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using FARM_ATTENDANCE_SYSTEM.Data;
using FARM_ATTENDANCE_SYSTEM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FARM_ATTENDANCE_SYSTEM.Controllers
{
    public class HomeController : Controller
    {


        private readonly FarmDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher; // ✅ Change to User

        public HomeController(FarmDbContext context, IPasswordHasher<User> passwordHasher) // ✅ Change to User
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes("YourSecureSecretKey12345678901234567890"); // ✅ Ensure this matches Program.cs
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // ✅ Ensure user has an Id
                }),
                Expires = DateTime.UtcNow.AddHours(1), // ✅ Token expiration time
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (user != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

                if (result == PasswordVerificationResult.Success)
                {
                    // Store session info
                    HttpContext.Session.SetString("UserName", user.UserName);
                    HttpContext.Session.SetString("UserRole", user.UserGroup);

                    TempData["Success"] = "Login successful!";
                    return RedirectToAction("Dashboard", "Home");
                }
            }

            TempData["Err"] = "Invalid username or password";
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Home/Index");
        }

        public IActionResult Privacy()
        {

            return View();
        }


        public IActionResult Dashboard()
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            ViewBag.allListofFarmers = _context.Farmers.Count();
            ViewBag.allFarmersAudit = _context.Farmers.Count();

            // Count male and female farmers
            int maleCount = _context.Farmers.Count(f => f.GENDER == "M");
            int femaleCount = _context.Farmers.Count(f => f.GENDER == "F");
            int total = maleCount + femaleCount;

            ViewBag.MaleCount = maleCount;
            ViewBag.FemaleCount = femaleCount;


            // Calculate percentages
            ViewBag.MalePercentage = total > 0 ? Math.Round((double)maleCount * 100 / total, 1) : 0;
            ViewBag.FemalePercentage = total > 0 ? Math.Round((double)femaleCount * 100 / total, 1) : 0;

            return View();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
