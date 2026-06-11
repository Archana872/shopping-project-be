using Microsoft.Data.SqlClient;
using WebApplication1.DataModel;

namespace WebApplication1.RepositryLayer;

public class ProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DBConnectionString")
            ?? throw new InvalidOperationException("DBConnectionString is not configured.");
    }

    public void AddProduct(Product product)
    {
        const string sql = "INSERT INTO Products (Name, Price, Stock) VALUES (@Name, @Price, @Stock)";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Price", product.Price);
        command.Parameters.AddWithValue("@Stock", product.Stock);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<Product> GetAllProducts()
    {
        const string sql = "SELECT Id, Name, Price, Stock FROM Products";
        var products = new List<Product>();

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);

        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            products.Add(MapProduct(reader));
        }

        return products;
    }

    public Product? GetProductById(int id)
    {
        const string sql = "SELECT Id, Name, Price, Stock FROM Products WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        using var reader = command.ExecuteReader();

        return reader.Read() ? MapProduct(reader) : null;
    }

    public void UpdateProduct(Product product)
    {
        const string sql = "UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", product.Id);
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Price", product.Price);
        command.Parameters.AddWithValue("@Stock", product.Stock);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void DeleteProduct(int id)
    {
        const string sql = "DELETE FROM Products WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Product MapProduct(SqlDataReader reader)
    {
        return new Product
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
            Stock = reader.GetInt32(reader.GetOrdinal("Stock"))
        };
    }
}
