using Microsoft.AspNetCore.Mvc;
using WebApplication1.BusinessLogic;
using WebApplication1.DataModel;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api")]
public class AppController : ControllerBase
{
    private readonly UserService _userService;

    public AppController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("users")]
    public ActionResult<UserResponse> CreateUser(CreateUserRequest request)
    {
        var user = _userService.CreateUser(request);

        if (user is null)
        {
            return Conflict("User already exists.");
        }

        return Ok(user);
    }

    [HttpPost("users/login")]
    public ActionResult<UserResponse> Login(LoginRequest request)
    {
        var user = _userService.Login(request);

        if (user is null)
        {
            return Unauthorized("Invalid name or password.");
        }

        return Ok(user);
    }

    [HttpPost("CreateOrder")]
    public ActionResult<UserResponse> CreateOrder(CreateUserRequest request)
    {
        var user = _userService.CreateUser(request);

        if (user is null)
        {
            return Conflict("User already exists.");
        }

        return Ok(user);
    }
}
