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
    [HttpPost("Insertitems")]
    public IActionResult AddItem(ItemRequest request)
    {
        _itemRepository.AddItem(request);
        return Ok(new { message = "Item added successfully" });
    }

    [HttpGet("Getitems")]
    public IActionResult GetItems()
    {
        var items = _itemRepository.GetItems();

        if (items == null || !items.Any())
        {
            return NotFound(new { message = "No items found." });
        }

        return Ok(items);
    }
    
    [HttpGet("items/{itemName}")]
    public IActionResult GetItemByName(string itemName)
    {
        var item = _itemRepository.GetItemByName(itemName);

        if (item == null)
        {
            return NotFound(new { message = "Item not found." });
        }

        return Ok(item);
    }
}