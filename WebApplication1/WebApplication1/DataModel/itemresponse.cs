

namespace WebApplication1.DataModel;
public class ItemResponse
{
    public int Id { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Measurement { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
