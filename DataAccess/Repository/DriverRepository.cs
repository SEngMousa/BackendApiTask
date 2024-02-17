using System.Data.SQLite;
using Domain.Entities;

namespace DataAccess;

public class DriverRepository :IDriverRepository
    {
        private readonly DatabaseContext _context;

        public DriverRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Add(Driver driver)
        {
            using (var connection = new SQLiteConnection(_context.ConnectionString))
            {
                connection.Open();
                var sql = @"INSERT INTO Drivers (FirstName, LastName, Email, PhoneNumber) 
                            VALUES (@FirstName, @LastName, @Email, @PhoneNumber);";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", driver.FirstName);
                    command.Parameters.AddWithValue("@LastName", driver.LastName);
                    command.Parameters.AddWithValue("@Email", driver.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", driver.PhoneNumber);
                   await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Driver> GetById(int id)
        {
            using (var connection = new SQLiteConnection(_context.ConnectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Drivers WHERE Id = @Id;";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if ( await reader.ReadAsync())
                        {
                            return new Driver
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"]),
                                Email = Convert.ToString(reader["Email"]),
                                PhoneNumber = Convert.ToString(reader["PhoneNumber"])
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        

        
        public async Task<IEnumerable<Driver>> GetAll()
        {
            var drivers = new List<Driver>();
            using (var connection = new SQLiteConnection(_context.ConnectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Drivers;";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            drivers.Add(new Driver
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"]),
                                Email = Convert.ToString(reader["Email"]),
                                PhoneNumber = Convert.ToString(reader["PhoneNumber"])
                            });
                        }
                    }
                }
            }
            return drivers;
        }
        
        public async Task<IEnumerable<Driver>> GetAllAlphabetically()
        {
            var drivers = new List<Driver>();
            using (var connection = new SQLiteConnection(_context.ConnectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Drivers ORDER BY FirstName, LastName;";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            drivers.Add(new Driver
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"]),
                                Email = Convert.ToString(reader["Email"]),
                                PhoneNumber = Convert.ToString(reader["PhoneNumber"])
                            });
                        }
                    }
                }
            }
            return drivers;
        }

        public async Task Update(Driver driver)
        {
            using (var connection = new SQLiteConnection(_context.ConnectionString))
            {
                connection.Open();
                var sql = @"UPDATE Drivers 
                            SET FirstName = @FirstName, LastName = @LastName, 
                                Email = @Email, PhoneNumber = @PhoneNumber 
                            WHERE Id = @Id;";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", driver.FirstName);
                    command.Parameters.AddWithValue("@LastName", driver.LastName);
                    command.Parameters.AddWithValue("@Email", driver.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", driver.PhoneNumber);
                    command.Parameters.AddWithValue("@Id", driver.Id);
                   await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SQLiteConnection(_context.ConnectionString))
            {
                connection.Open();
                var sql = "DELETE FROM Drivers WHERE Id = @Id;";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        
        
    }