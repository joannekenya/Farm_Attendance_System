using FARM_ATTENDANCE_SYSTEM.Data;
using FARM_ATTENDANCE_SYSTEM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenProvider
{
    private readonly FarmDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public TokenProvider(FarmDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _passwordHasher = new PasswordHasher<User>();
    }

    public string? LoginUser(string UserName, string Password)
    {
        if (_context.Users == null)
        {
            throw new Exception("Database context Users table is not initialized.");
        }

        var user = _context.Users.SingleOrDefault(x => x.UserName == UserName);
        if (user == null)
        {
            Console.WriteLine("Invalid password.");
            return null; // User not found
        }

        // Verify hashed password
        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            return null; // Invalid password
        }

        // ✅ Use a stronger secret key (at least 32 characters)
        var secretKey = "ThisIsAStrongSuperSecretKeyForJWT!123456"; // Replace with a real secret key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var JWToken = new JwtSecurityToken(
            issuer: "YourApp",  // Set issuer
            audience: "YourAppUsers", // Set audience
            claims: GetUserClaims(user),
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(JWToken);
    }

    private IEnumerable<Claim> GetUserClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Standard ID claim
            new Claim(ClaimTypes.Name, user.UserName), // Store username
            new Claim(ClaimTypes.Role, user.UserGroup) // Role-based authorization
        };

        // ✅ Ensure the UserGroup claim has a valid namespace
        if (!string.IsNullOrEmpty(user.UserGroup))
        {
            claims.Add(new Claim("http://schemas.yourapp.com/usergroup", user.UserGroup));
        }

        return claims;
    }
}
