namespace Database;

using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public Domain.User ToDomain()
    {
        return new Domain.User
        {
            Username = Email,
            Password = Password
        };
    }
}

public class UserRepository(string connectionString)
{
    public async Task<Domain.User> GetUserByEmailAsync(string email)
    {
        using (IDbConnection db = new SqliteConnection(connectionString))
        {
            const string sql = "SELECT Id, Email, Password FROM User WHERE Email = @Email";
            var dbUser =  await db.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
            return dbUser.ToDomain();
        }
    }
}
