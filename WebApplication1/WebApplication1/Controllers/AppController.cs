using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
    private readonly IConfiguration _configuration;

    public AppController(
        UserService userService,
        ItemRepository itemRepository,
        IConfiguration configuration)
    {
        _userService = userService;
        _itemRepository = itemRepository;
        _configuration = configuration;
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

    [HttpPost("Insertitems")]
    public IActionResult InsertItem(ItemRequest request)
    {
        _itemRepository.Item(request);

        return Ok(new
        {
            Message = "Item added successfully"
        });
    }

    [HttpGet("Getitems")]
    public IActionResult GetItems()
    {
        var items = _itemRepository.GetItems();
        return Ok(items);
    }

    [HttpGet("Getitems/{itemName}")]
    public IActionResult GetItemByName(string itemName)
    {
        var item = _itemRepository.GetItemByName(itemName);

        if (item == null)
        {
            return NotFound("Item not found");
        }

        return Ok(item);
    }
    [HttpGet("testdb")]
    public IActionResult TestDb()
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DBConnectionString"));
            connection.Open();
            return Ok("Database Connected");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("stock")]
    public IActionResult GetStock()
    {
        var stockItems = _itemRepository.GetStockItems();

        return Ok(stockItems);
    }
    [HttpPut("updatestock")]
    public IActionResult UpdateStock(UpdateStockRequest request)
    {
        int rows = _itemRepository.UpdateStock(
            request.ItemName,
            request.AvailableQuantity);

        return Ok($"Rows Updated: {rows}");
    }
}