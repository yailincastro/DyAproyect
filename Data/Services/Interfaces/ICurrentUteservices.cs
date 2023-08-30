
using DyAproyect.Data.Response;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DyAproyect.Data.Services.Interfaces
{

public interface ICurrentUserService
{
    Task<string> Nombre();
    Task<string> Role();
    Task<int> UserId();
}

public class CurrentUserService : ICurrentUserService
{
    private readonly ProtectedSessionStorage _sessionStorage;
    public CurrentUserService(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    private async Task<UsuarioResponse> GetUserDataAsync()
    {
        var userDataStorage = await _sessionStorage.GetAsync<UsuarioResponse>("UserToken");
        return userDataStorage!.Success ? userDataStorage!.Value! : new UsuarioResponse();
    }
    public async Task<int> UserId()
    {
        var userData = await GetUserDataAsync();
        return userData?.Id ?? 0;
    }
    public async Task<string> Nombre()
    {
        var userData = await GetUserDataAsync();
        return userData?.Nombre ?? "Guest";

    }
    public async Task<string> Role()
    {
        var userData = await GetUserDataAsync();
        return userData?.Role ?? "";

    }
}
}