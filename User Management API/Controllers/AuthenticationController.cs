using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace User_Management_API.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController(IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    
    public class AuthenticationRequestBody
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    private new class User(int userId, string userName, string firstName, string lastName)
    {
        public int UserId { get; set; } = userId;
        public string UserName { get; set; } = userName;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
    {
        var user = ValidateUserCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);

        var securityKey =
            new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]!));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>
        {
            new Claim("sub", user.UserId.ToString()),
            new Claim("given_name", user.FirstName),
            new Claim("family_name", user.LastName)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return Ok(tokenToReturn);
    }

    private User ValidateUserCredentials(string? userName, string? password)
    {
        return new User(1, "mk17b", "Mark", "Bueno");
    }
}