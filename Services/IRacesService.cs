using SynopsisApp.Models;

namespace SynopsisApp.Services
{
    public interface IRacesService
    {
        Task<List<RaceItem>> GetCurrentSeasonRaces();
        Task<List<RaceItem>> GetNextRace();
        Task<List<RaceItem>> SearchSeasonByYear(string year);
    }
}
