﻿using BlazorBattles.Shared;
using Blazored.Toast.Services;

namespace BlazorBattles.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toastService;

        public UnitService(IToastService toastService)
        {
            _toastService = toastService;
        }
        public IList<Unit> Units => new List<Unit>
        {
            new Unit { Id = 1, Title = "Knight", Attack = 10, Defanse = 10, BananaConst = 100 },
            new Unit { Id = 2, Title = "Archer", Attack = 15, Defanse = 5, BananaConst = 150 },
            new Unit { Id = 3, Title = "Mage", Attack = 20, Defanse = 1, BananaConst = 200 },
        };
        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();

        public void AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
            MyUnits.Add(new UserUnit { UnitId = unit.Id, HitPoints = unit.HitPoints });
            _toastService.ShowSuccess($"Orduya {unit.Title} eklendi!", "Ordu Bildirimi");
        }
    }
}
