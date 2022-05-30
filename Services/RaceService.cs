using SynopsisApp.Models;
using System.Text.Json;

namespace SynopsisApp.Services
{
    public class RaceService : IRacesService
    {
        private readonly HttpClient _httpClient;
        const string _baseUrl = "https://ergast.com/api/f1/";
        const string _currentSeasonEndpoint = "current.json";
        const string _nextRaceEndpoint = "current/next.json";
        

        public RaceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //HTTPClient.BaseAddress kan ikke sættes i hver metode da den ikke kan ændres
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<List<RaceItem>> GetCurrentSeasonRaces()
        { 

            var response = await _httpClient.GetAsync(_currentSeasonEndpoint);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();

            var dto = await JsonSerializer.DeserializeAsync<RaceDto>(stream);
            return dto.MRData.RaceTable.Races.Select(race => new RaceItem
            {
                Season = race.season,
                RaceName = race.raceName,
                CircuitName = race.Circuit.circuitName,
                Date = race.date,
                Country = race.Circuit.Location.country,
                Url = race.url
            }).ToList();
        }

        public async Task<List<RaceItem>> GetNextRace()
        {
            var response = await _httpClient.GetAsync(_nextRaceEndpoint);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();

            var dto = await JsonSerializer.DeserializeAsync<RaceDto>(stream);
            return dto.MRData.RaceTable.Races.Select(race => new RaceItem
            {
                Season = race.season,
                RaceName = race.raceName,
                CircuitName = race.Circuit.circuitName,
                Date = race.date,
                Country = race.Circuit.Location.country,
                Url = race.url,
            }).ToList();
        }

        public async Task<List<RaceItem>> SearchSeasonByYear(string year)
        {
            var response = await _httpClient.GetAsync(year + ".json");
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();

            var dto = await JsonSerializer.DeserializeAsync<RaceDto>(stream);
            return dto.MRData.RaceTable.Races.Select(race => new RaceItem
            {
                Season = race.season,
                RaceName = race.raceName,
                CircuitName = race.Circuit.circuitName,
                Date = race.date,
                Country = race.Circuit.Location.country,
                Url = race.url,
            }).ToList();
        }
    }
}
