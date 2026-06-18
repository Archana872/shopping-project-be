namespace WebApplication1.DataModel;

public class UpdateStockRequest
{
    public string ItemName { get; set; } = string.Empty;
    public decimal AvailableQuantity { get; set; }
}
