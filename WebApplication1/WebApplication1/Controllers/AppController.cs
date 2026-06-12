using Microsoft.AspNetCore.Mvc;
using WebApplication1.BusinessLogic;
using WebApplication1.DataModel;
using WebApplication1.RepositryLayer;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api")]
public class AppController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ItemRepository _itemRepository;

    public AppController(UserService userService, ItemRepository itemRepository)
    {
        _userService = userService;
        _itemRepository = itemRepository;
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

    [HttpPost("items")]
    public IActionResult AddItem(ItemRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.ItemName) || string.IsNullOrWhiteSpace(request.Measurement))
        {
            return BadRequest("ItemName and Measurement are required.");
        }

        _itemRepository.AddItem(request);

        return Ok(new { message = "Item added successfully" });
    }
    [HttpGet("items")]
    public IActionResult GetItems()
    {
        var items = _itemRepository.GetItems();

        return Ok(items);
    }
    [HttpGet("items/{id}")]
    public IActionResult GetItemById(int id)
    {
        var item = _itemRepository.GetItemById(id);

        if (item == null)
        {
            return NotFound(new { message = "Item not found." });
        }

        return Ok(item);
    }

}
