namespace WebApplication1.DataModel;

public class AddItemRequest
{
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Measurement { get; set; } = string.Empty;
}
