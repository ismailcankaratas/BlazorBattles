namespace BlazorBattles.Client.Services
{
    public interface IBananaService
    {
        event Action OnChange;
        int Bananas { get; set; }
        void EatBananas(int amount);
        Task AddBananas(int amount);
        Task GetBananas();
    }
}
