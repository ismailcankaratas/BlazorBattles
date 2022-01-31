using BlazorBattles.Shared;

namespace BlazorBattles.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request); 
        Task<ServiceResponse<string>> Login(UserLogin request);
    }
}
