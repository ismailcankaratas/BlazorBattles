using BlazorBattles.Shared;
using System.Net.Http.Json;

namespace BlazorBattles.Client.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly HttpClient _http;

        public LeaderboardService(HttpClient http)
        {
            _http = http;
        }

        public IList<UserStatistic> Leaderboard { get; set; }

        public async Task GetLeaderboard()
        {
            Leaderboard = await _http.GetFromJsonAsync<IList<UserStatistic>>("api/user/leaderboard");

        }
    }
}
