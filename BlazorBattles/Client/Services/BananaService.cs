﻿namespace BlazorBattles.Client.Services
{
    public class BananaService : IBananaService
    {
        public event Action OnChange;
        public int Bananas { get; set; } = 1000;

        public void EatBananas(int amount)
        {
            Bananas -= amount;
            BananasChanged();
        }
        public void AddBananas(int amount)
        {
            Bananas += amount;
            BananasChanged();
        }
        void BananasChanged() => OnChange.Invoke();
    }
}
