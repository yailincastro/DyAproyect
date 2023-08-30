using DyAproyect.Data.Response; 
using Microsoft.AspNetCore.Components.Authorization;

namespace DyAproyect.Autenticacion
{

    public interface IAutenticacion
{
    Task<AuthenticationState> GetAuthenticationStateAsync();
    Task UpdateAuthenticationState(UsuarioResponse userData);
}

}