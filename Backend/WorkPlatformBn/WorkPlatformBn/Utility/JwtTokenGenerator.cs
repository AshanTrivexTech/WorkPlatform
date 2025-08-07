using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WorkPlatformBn.Utility;

public class JwtTokenGenerator
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtTokenGenerator(string secretKey, string issuer, string audience)
    {
        _secretKey = secretKey;
        _issuer = issuer;
        _audience = audience;
    }

    public string GenerateJwtToken(string username,string email)
    {
        // Create claims based on the user information (for example, username)
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),  // Username
            new Claim(ClaimTypes.Email, email),  // Name identifier
            new Claim(ClaimTypes.Role, "User")  // Assign a role (optional)
        };

        // Create a symmetric security key based on the secret key
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

        // Signing credentials using HMACSHA256
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Generate the token
        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),  // Token expiration time (e.g., 1 hour)
            signingCredentials: credentials
        );

        // Return the token as a string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
