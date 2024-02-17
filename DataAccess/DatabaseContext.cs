using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
namespace DataAccess;

public class DatabaseContext
{
    private readonly IConfiguration _configuration;

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
        ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        InitializeDatabase();
    }

    public string ConnectionString { get; }

    private void InitializeDatabase()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var createTableCommand = @"
                    CREATE TABLE IF NOT EXISTS Drivers (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        PhoneNumber TEXT NOT NULL
                    );";
            var command = connection.CreateCommand();
            command.CommandText = createTableCommand;
            command.ExecuteNonQuery();
        }
    }

    public SqliteConnection GetConnection()
    {
        return new SqliteConnection(ConnectionString);
    }
}