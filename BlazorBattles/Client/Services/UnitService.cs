using BlazorBattles.Shared;
using Blazored.Toast.Services;
using System.Net.Http.Json;

namespace BlazorBattles.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _http;
        private readonly IBananaService _bananaService;

        public UnitService(IToastService toastService, HttpClient http, IBananaService bananaService)
        {
            _toastService = toastService;
            _http = http;
            _bananaService = bananaService;
        }
        public IList<Unit> Units { get; set; } = new List<Unit>();
        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();

        public async Task AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
            var result = await _http.PostAsJsonAsync<int>("api/userunit", unitId);

            if(result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                await _bananaService.GetBananas();
                _toastService.ShowSuccess($"Orduya {unit.Title} eklendi!", "Ordu Bildirimi");
            }
        }

        public async Task LoadUnitAsync()
        {
            if(Units.Count == 0)
            {
                Units = await _http.GetFromJsonAsync<IList<Unit>>("api/Unit");
            }
        }

        public async Task LoadUserUnitsAsync()
        {
            MyUnits = await _http.GetFromJsonAsync<IList<UserUnit>>("api/userunit");
        }

        public async Task ReviveArmy()
        {
            var result = await _http.PostAsJsonAsync<string>("api/userunit/revive", null);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                _toastService.ShowSuccess(await result.Content.ReadAsStringAsync());
            else
                _toastService.ShowError(await result.Content.ReadAsStringAsync());

            await LoadUserUnitsAsync();
            await _bananaService.GetBananas();
        }
    }
}
