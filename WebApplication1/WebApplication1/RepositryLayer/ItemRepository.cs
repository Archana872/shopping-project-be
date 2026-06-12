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

    public List<ItemResponse> GetItems()
    {
        var items = new List<ItemResponse>();

        const string sql =
            "SELECT ItemName, Quantity, Measurement, Price FROM Item";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);

        connection.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            items.Add(new ItemResponse
            {
                ItemName = reader["ItemName"]?.ToString() ?? string.Empty,
                Quantity = Convert.ToDecimal(reader["Quantity"]),
                Measurement = reader["Measurement"]?.ToString() ?? string.Empty,
                Price = Convert.ToDecimal(reader["Price"])
            });
        }

        return items;
    }

    public ItemResponse? GetItemByName(string itemName)
    {
        const string sql =
            "SELECT ItemName, Quantity, Measurement, Price FROM Item WHERE ItemName = @ItemName";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@ItemName", itemName);

        connection.Open();

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new ItemResponse
            {
                ItemName = reader["ItemName"]?.ToString() ?? string.Empty,
                Quantity = Convert.ToDecimal(reader["Quantity"]),
                Measurement = reader["Measurement"]?.ToString() ?? string.Empty,
                Price = Convert.ToDecimal(reader["Price"])
            };
        }

        return null;
    }
    public void AddItem(ItemRequest item)
    {
        const string sql =
            "INSERT INTO Item (ItemName, Quantity, Measurement, Price) VALUES (@ItemName, @Quantity, @Measurement, @Price)";

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
