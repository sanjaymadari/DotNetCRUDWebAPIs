using Dapper;
using DotNetCRUDWebAPIs.Models;

namespace DotNetCRUDWebAPIs.Repositories;

public interface IUserRepository
{
    // CRUD Operations - Create, Read, Update, Delete
    // Create
    Task<User> CreateUser(User user);
    // Read
    Task<List<User>> GetUsers();
    Task<User?> GetUserById(long id);
    // Update
    Task<bool> UpdateUser(User user);
    // Delete
    Task<bool> DeleteUser(long id);
}

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<User> CreateUser(User user)
    {
        var query = @"INSERT INTO users (username, full_name, email, phone, password) 
        VALUES (@Username, @FullName, @Email, @Phone, @Password) RETURNING *";

        using var connection = DbConnection;
        var res = await connection.QueryFirstOrDefaultAsync<User>(query, user);

        return res!;
    }

    public async Task<bool> DeleteUser(long id)
    {
        var query = @"DELETE FROM users WHERE id = @Id";

        using var connection = DbConnection;
        var res = await connection.ExecuteAsync(query, new { Id = id });

        return res > 0;
    }

    public async Task<User?> GetUserById(long id)
    {
        var query = @"SELECT * FROM users WHERE id = @Id";

        using var connection = DbConnection;
        var res = await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });

        return res;
    }

    public async Task<List<User>> GetUsers()
    {
        var query = @"SELECT * FROM users";

        using var connection = DbConnection;
        var res = await connection.QueryAsync<User>(query);

        return res.AsList();
    }

    public async Task<bool> UpdateUser(User user)
    {
        var query = @"UPDATE users SET username = @Username, full_name = @FullName, email = @Email, 
        phone = @Phone, password = @Password, updated_at = @UpdatedAt WHERE id = @Id";

        using var connection = DbConnection;
        var res = await connection.ExecuteAsync(query, user);

        return res > 0;
    }
}
