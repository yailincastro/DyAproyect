
namespace DyAproyect.Data.Services.Interfaces
{
public interface IImagenService
{
    Task<int> GuardarImagenAsync(string datosBase64);
    Task<string> ObtenerImagenAsync(int id);
}
}