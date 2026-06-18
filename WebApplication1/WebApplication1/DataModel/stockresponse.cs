public class StockResponse
{
    public int StockId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal AvailableQuantity { get; set; }
    public string Measurement { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
