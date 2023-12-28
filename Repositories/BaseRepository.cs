using Npgsql;

namespace DotNetCRUDWebAPIs.Repositories;

public class BaseRepository
{
    private readonly IConfiguration _configuration;

    public BaseRepository(IConfiguration configuration)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        _configuration = configuration;
    }

    // TODO: Change the connection string to your own and it can be stored in appsettings.json
    public NpgsqlConnection DbConnection => new("Host=localhost;Port=5432;Username=madarisanjay;Password=password;Database=postgres");
}
