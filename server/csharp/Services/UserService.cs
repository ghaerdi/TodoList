using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace todolist.Services;

public class UserService
{
    protected readonly string _tokenKey = "access_token";
    protected readonly IConfiguration _configuration;

    public UserService(IConfiguration configuration) =>
        _configuration = configuration;

    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPassword(string password, string hashPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashPassword);

    public string? GetToken(HttpContext httpContext) =>
        httpContext.Request.Cookies[_tokenKey];

    public void SetToken(HttpContext httpContext, UserEntity user) =>
        httpContext.Response.Cookies.Append(_tokenKey, _generateJWT(user));

    public void DeleteToken(HttpContext httpContext) =>
        httpContext.Response.Cookies.Delete(_tokenKey);

    protected string _generateJWT(UserEntity user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("sub", user.Id.ToString()),
            new Claim("username", user.Username),
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        SigningCredentials signIn = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: signIn
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
