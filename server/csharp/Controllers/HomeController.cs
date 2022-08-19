using Microsoft.AspNetCore.Mvc;
using todolist.Helpers;

namespace todolist.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Welcome()
    {
        ResponseHelper response = new(MessageHelper.Success.Welcome);
        return Ok(response);
    }
}
