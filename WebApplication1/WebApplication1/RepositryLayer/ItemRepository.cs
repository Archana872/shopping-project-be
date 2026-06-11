using Microsoft.Data.SqlClient;
using WebApplication1.DataModel;

namespace WebApplication1.RepositryLayer;

public class ItemRepository
{
    private readonly string _connectionString;

    public ItemRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DBConnectionString")
            ?? throw new InvalidOperationException("DBConnectionString is not configured.");
    }

    public void AddItem(ItemRequest item)
    {
        const string sql = "INSERT INTO Item (ItemName, Quantity, Measurement,Price) VALUES (@ItemName, @Quantity, @Measurement, @Price)";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ItemName", item.ItemName);
        command.Parameters.AddWithValue("@Quantity", item.Quantity);
        command.Parameters.AddWithValue("@Measurement", item.Measurement);
        command.Parameters.AddWithValue("@Price", item.Price);

        connection.Open();
        command.ExecuteNonQuery();
    }
}
