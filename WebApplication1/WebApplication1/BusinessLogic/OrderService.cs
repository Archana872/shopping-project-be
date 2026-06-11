using WebApplication1.DataModel;
using WebApplication1.RepositryLayer;

namespace WebApplication1.BusinessLogic;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly ProductRepository _productRepository;

    public OrderService(OrderRepository orderRepository, ProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public string? CreateOrder(CreateOrderRequest request)
    {
        // Validate ProductId
        if (request.ProductId <= 0)
        {
            return "Product ID must be greater than 0.";
        }

        // Get product to verify it exists and get its price
        var product = _productRepository.GetProductById(request.ProductId);
        if (product is null)
        {
            return "Product not found.";
        }

        // Validate quantity
        if (request.Quantity <= 0)
        {
            return "Quantity must be greater than 0.";
        }

        // Check if enough stock is available
        if (request.Quantity > product.Stock)
        {
            return $"Insufficient stock. Available: {product.Stock}, Requested: {request.Quantity}";
        }

        // Calculate total price
        var totalPrice = product.Price * request.Quantity;

        // Create order
        var order = new Order
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            TotalPrice = totalPrice,
            Status = "Order Placed",
            OrderDate = DateTime.Now
        };

        _orderRepository.CreateOrder(order);
        return null;
    }

    public List<Order> GetAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }

    public Order? GetOrderById(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        return _orderRepository.GetOrderById(id);
    }

    public string? UpdateOrderStatus(int id, string status)
    {
        if (id <= 0)
        {
            return "Invalid order ID.";
        }

        if (string.IsNullOrWhiteSpace(status))
        {
            return "Status is required.";
        }

        var validStatuses = new[] { "Order Placed", "Confirmed", "Shipped", "Delivered", "Cancelled" };
        if (!validStatuses.Contains(status))
        {
            return $"Invalid status. Valid statuses: {string.Join(", ", validStatuses)}";
        }

        var existingOrder = _orderRepository.GetOrderById(id);
        if (existingOrder is null)
        {
            return "Order not found.";
        }

        _orderRepository.UpdateOrderStatus(id, status);
        return null;
    }

    public string? DeleteOrder(int id)
    {
        if (id <= 0)
        {
            return "Invalid order ID.";
        }

        var existingOrder = _orderRepository.GetOrderById(id);
        if (existingOrder is null)
        {
            return "Order not found.";
        }

        _orderRepository.DeleteOrder(id);
        return null;
    }
}
