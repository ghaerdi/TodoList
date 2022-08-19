using Microsoft.AspNetCore.Mvc;
using todolist.Repositories;
using todolist.Services;
using todolist.Helpers;
using todolist.DTOs;
using AutoMapper;

namespace todolist.Controllers;

[ApiController]
[Route("")]
public class UserController : ControllerBase
{
    protected readonly UserRepository _repository;
    protected readonly UserService _service;
    protected readonly IMapper _mapper;

    public UserController(UserService service, UserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public IActionResult Register(UserRegisterDTO user)
    {
        if (_repository.EmailExist(user.Email))
            return BadRequest(new ResponseHelper($"Email {MessageHelper.Error.AlreadyExist}", true));

        if (_repository.UsernameExist(user.Username))
            return BadRequest(new ResponseHelper($"Username {MessageHelper.Error.AlreadyExist}", true));

        user.Password = _service.HashPassword(user.Password);

        _repository.Create(_mapper.Map<UserEntity>(user));
        ResponseHelper response = new($"User {MessageHelper.Success.Created}");
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(UserLoginDTO user)
    {
        var match = _repository.GetByUsername(user.Username);

        if (match == null)
            return BadRequest(new ResponseHelper($"User {MessageHelper.Error.NotFound}", true));

        if (!_service.VerifyPassword(user.Password, match.Password))
            return BadRequest(new ResponseHelper("Invalid password", true));

        _service.SetToken(HttpContext, match);

        return Ok(new ResponseHelper("User logged"));
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        _service.DeleteToken(HttpContext);
        return Ok(new ResponseHelper("User logout"));
    }
}
