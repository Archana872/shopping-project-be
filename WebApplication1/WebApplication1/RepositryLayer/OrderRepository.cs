using Microsoft.Data.SqlClient;
using WebApplication1.DataModel;

namespace WebApplication1.RepositryLayer;

public class OrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DBConnectionString")
            ?? throw new InvalidOperationException("DBConnectionString is not configured.");
    }

    public void CreateOrder(Order order)
    {
        const string sql = "INSERT INTO Orders (ProductId, Quantity, TotalPrice, Status, OrderDate) VALUES (@ProductId, @Quantity, @TotalPrice, @Status, @OrderDate)";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ProductId", order.ProductId);
        command.Parameters.AddWithValue("@Quantity", order.Quantity);
        command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
        command.Parameters.AddWithValue("@Status", order.Status);
        command.Parameters.AddWithValue("@OrderDate", order.OrderDate);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<Order> GetAllOrders()
    {
        const string sql = "SELECT Id, ProductId, Quantity, TotalPrice, Status, OrderDate FROM Orders ORDER BY OrderDate DESC";
        var orders = new List<Order>();

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);

        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            orders.Add(MapOrder(reader));
        }

        return orders;
    }

    public Order? GetOrderById(int id)
    {
        const string sql = "SELECT Id, ProductId, Quantity, TotalPrice, Status, OrderDate FROM Orders WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        using var reader = command.ExecuteReader();

        return reader.Read() ? MapOrder(reader) : null;
    }

    public void UpdateOrderStatus(int id, string status)
    {
        const string sql = "UPDATE Orders SET Status = @Status WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Status", status);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void DeleteOrder(int id)
    {
        const string sql = "DELETE FROM Orders WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Order MapOrder(SqlDataReader reader)
    {
        return new Order
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
            TotalPrice = reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
            Status = reader.GetString(reader.GetOrdinal("Status")),
            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate"))
        };
    }
}
