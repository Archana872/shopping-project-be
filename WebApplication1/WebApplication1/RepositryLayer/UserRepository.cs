using Microsoft.Data.SqlClient;
using WebApplication1.DataModel;

namespace WebApplication1.RepositryLayer;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DBConnectionString")
            ?? throw new InvalidOperationException("DBConnectionString is not configured.");
    }

    public User? GetByName(string name)
    {
        const string sql = "SELECT Name, Password, Role FROM Users WHERE Name = @Name";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Name", name);

        connection.Open();
        using var reader = command.ExecuteReader();

        return reader.Read() ? MapUser(reader) : null;
    }

    public User? GetByNameAndPassword(string name, string password)
    {
        const string sql = "SELECT Name, Password, Role FROM Users WHERE Name = @Name AND Password = @Password";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Password", password);

        connection.Open();
        using var reader = command.ExecuteReader();

        return reader.Read() ? MapUser(reader) : null;
    }

    public void Create(User user)
    {
        const string sql = "INSERT INTO Users (Name, Password, Role) VALUES (@Name, @Password, @Role)";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Name", user.Name);
        command.Parameters.AddWithValue("@Password", user.Password);
        command.Parameters.AddWithValue("@Role", user.Role.ToString());

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static User MapUser(SqlDataReader reader)
    {
        return new User
        {
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Password = reader.GetString(reader.GetOrdinal("Password")),
            Role = Enum.Parse<UserRole>(reader.GetString(reader.GetOrdinal("Role")))
        };
    }
}
