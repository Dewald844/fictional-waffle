namespace Database;

using Microsoft.Data.Sqlite;
using System.Data;
using Dapper;

public class OlympicWinner
{
    public string athlete { get; set; }
    public int? age { get; set; }
    public string country { get; set; }
    public int? year { get; set; }
    public string date { get; set; }
    public string sport { get; set; }
    public int? gold { get; set; }
    public int? silver { get; set; }
    public int? bronze { get; set; }
    public int? total { get; set; }

}

public class OlympicWinnerRepository(string connectionString)
{
    public async Task<List<OlympicWinner>> GetOlympicWinnersAsync()
    {
        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        const string sql = "SELECT Athlete, Age, Country, Year, Date, Sport, Gold, Silver, Bronze, Total FROM OlympicWinners";
        var winners = await connection.QueryAsync<OlympicWinner>(sql);

        return winners.AsList();
    }

    public async Task InsertOlympicWinnersDapper(List<OlympicWinner> winners)
    {
        using (IDbConnection db = new SqliteConnection(connectionString))
        {
            var sql = @"
            INSERT INTO OlympicWinners (Athlete, Age, Country, Year, Date, Sport, Gold, Silver, Bronze, Total)
            VALUES (@athlete, @age, @country, @year, @date, @sport, @gold, @silver, @bronze, @total);";

            db.Open();

            // ExecuteAsync accepts IEnumerable of parameters for batch insert
            await db.ExecuteAsync(sql, winners);
        }
    }

    public async Task CreateTable()
    {
        await using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        await using var createTableCmd = connection.CreateCommand();
        createTableCmd.CommandText = @"
        CREATE TABLE IF NOT EXISTS OlympicWinners (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Athlete TEXT,
            Age INTEGER,
            Country TEXT,
            Year INTEGER,
            Date TEXT,
            Sport TEXT,
            Gold INTEGER,
            Silver INTEGER,
            Bronze INTEGER,
            Total INTEGER
        );";

        await createTableCmd.ExecuteNonQueryAsync();
    }
}
