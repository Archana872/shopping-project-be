using Microsoft.AspNetCore.Mvc;
using WebApplication1.BusinessLogic;
using WebApplication1.DataModel;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public IActionResult CreateOrder(CreateOrderRequest request)
    {
        var error = _orderService.CreateOrder(request);

        if (error is not null)
        {
            return BadRequest(new { message = error });
        }

        return Ok(new { message = "Order created successfully." });
    }

    [HttpGet]
    public IActionResult GetAllOrders()
    {
        var orders = _orderService.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public IActionResult GetOrderById(int id)
    {
        var order = _orderService.GetOrderById(id);

        if (order is null)
        {
            return NotFound(new { message = "Order not found." });
        }

        return Ok(order);
    }

    [HttpPut("{id}/status")]
    public IActionResult UpdateOrderStatus(int id, UpdateOrderStatusRequest request)
    {
        var error = _orderService.UpdateOrderStatus(id, request.Status);

        if (error is not null)
        {
            return BadRequest(new { message = error });
        }

        return Ok(new { message = "Order status updated successfully." });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        var error = _orderService.DeleteOrder(id);

        if (error is not null)
        {
            return BadRequest(new { message = error });
        }

        return Ok(new { message = "Order deleted successfully." });
    }
}
