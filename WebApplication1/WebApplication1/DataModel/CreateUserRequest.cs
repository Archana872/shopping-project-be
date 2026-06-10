namespace WebApplication1.DataModel;

public class CreateUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}

public class CreateOrderRequest
{
    public string item { get; set; }
    public int unitprice { get; set; }
    public int qty { get; set; }
}
