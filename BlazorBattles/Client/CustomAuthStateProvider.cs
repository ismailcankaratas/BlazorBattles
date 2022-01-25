using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorBattles.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "İsmail")
            }, "test");
            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));

        }
    }
}
