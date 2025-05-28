namespace Controller;

using Database;
using System.Net.Http;
using System.Text.Json;

public class OlympicWinnerService
{
    private readonly OlympicWinnerRepository _olympicWinnerRepository;

    public OlympicWinnerService(OlympicWinnerRepository olympicWinnerRepository)
    {
        _olympicWinnerRepository = olympicWinnerRepository;
    }

    private async Task<List<OlympicWinner>> FetchOlympicWinnersAsync()
    {
        var url = "https://www.ag-grid.com/example-assets/olympic-winners.json";

        using var httpClient = new HttpClient();

        // Set User-Agent header
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MyApp/1.0)");
        var json = await httpClient.GetStringAsync(url);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var winners = JsonSerializer.Deserialize<List<OlympicWinner>>(json, options);

        return winners;
    }

    private async Task ImportOlympicWinnersToSqlite()
    {
        var winners = await FetchOlympicWinnersAsync();

        _olympicWinnerRepository.CreateTable();
        _olympicWinnerRepository.InsertOlympicWinnersDapper(winners);
    }

    public async Task<List<OlympicWinner>> GetOlympicWinnersAsync()
    {
        var winners = await _olympicWinnerRepository.GetOlympicWinnersAsync();
        if (winners.Count == 0)
        {
            await ImportOlympicWinnersToSqlite();
            winners = await _olympicWinnerRepository.GetOlympicWinnersAsync();
        }
        return winners;
    }

}
