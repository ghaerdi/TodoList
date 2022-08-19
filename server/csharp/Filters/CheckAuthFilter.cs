using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using todolist.Repositories;
using todolist.Services;
using todolist.Helpers;
using System.Text;

namespace todolist.Filters;

public class CheckAuthFilter : IActionFilter
{
    private readonly UserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly UserService _userService;

    public CheckAuthFilter(UserRepository userRepository, IConfiguration configuration, UserService userService)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _userService = userService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var user = getUserFromContext(context);

        if (user == null) return;

        context.HttpContext.Items["user"] = user;
    }

    public void OnActionExecuted(ActionExecutedContext context) { }

    private UserEntity? getUserFromContext(ActionExecutingContext context)
    {
        var token = _userService.GetToken(context.HttpContext);

        var tokenString = "Token";

        if (string.IsNullOrEmpty(token))
        {
            ResponseHelper response = new($"{tokenString} {MessageHelper.Error.Required}", true);
            context.Result = new BadRequestObjectResult(response);
            return null;
        }

        var jwtToken = validateTokenClaims(context.HttpContext, token);

        if (jwtToken == null)
        {
            ResponseHelper response = new($"{token} {MessageHelper.Error.Invalid}", true);
            context.Result = new BadRequestObjectResult(response);
            return null;
        }

        var user = getUserFromJwtToken(jwtToken);

        if (user == null)
        {
            ResponseHelper response = new($"{tokenString} {MessageHelper.Error.InvalidUserRelation}", true);
            context.Result = new BadRequestObjectResult(response);
            return null;
        }

        return user;
    }

    private JwtSecurityToken? validateTokenClaims(HttpContext context, string token)
    {
        try
        {
            JwtSecurityTokenHandler handler = new();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken;
        }
        catch
        {
            return null;
        }
    }

    private UserEntity? getUserFromJwtToken(JwtSecurityToken token)
    {
        var userId = int.Parse(token.Claims.First(x => x.Type == "sub").Value);
        return _userRepository.GetById(userId);
    }
}
