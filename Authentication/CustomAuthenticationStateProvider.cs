using DyAproyect.Data.Response;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace DyAproyect.Autenticacion
{
    public class Autenticacion: AuthenticationStateProvider, IAutenticacion
      {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymus = new ClaimsPrincipal(new ClaimsIdentity());

        public Autenticacion(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userDataStorage = await _sessionStorage.GetAsync<UsuarioResponse>("UserToken");
                var userData = userDataStorage.Success ? userDataStorage.Value : null;
                if (userData == null)
                    return await Task.FromResult(new AuthenticationState(_anonymus));
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                        new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString()),
                        new Claim(ClaimTypes.Name, userData.Nombre),
                        new Claim(ClaimTypes.Email, userData.UserName),
                        new Claim(ClaimTypes.Role, userData.Role),
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymus));
            }
        }

        public async Task UpdateAuthenticationState(UsuarioResponse userData)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userData != null)
            {
                await _sessionStorage.SetAsync("UserToken", userData);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString()),
                        new Claim(ClaimTypes.Name, userData.Nombre),
                        new Claim(ClaimTypes.Email, userData.UserName),
                        new Claim(ClaimTypes.Role, userData.Role),
                    }));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserToken");
                claimsPrincipal = _anonymus;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
    
}