using DyAproyect.Data.Constant;
using DyAproyect.Data.Entities;
using DyAproyect.Data.Context;
using DyAproyect.Data.Services.Interfaces;

namespace DyAproyect.Data.Services
{
public class ImagenService : IImagenService
{
    private readonly IDyAproyectDbContext _context;

    public ImagenService(IDyAproyectDbContext context)
    {
        _context = context;
    }

    public async Task<int> GuardarImagenAsync(string datosBase64)
    {
        var imagen = new Imagen { DatosBase64 = datosBase64 };
        _context.Imagens.Add(imagen);
        await _context.SaveChangesAsync();
        return imagen.Id;
    }
    public async Task<string> ObtenerImagenAsync(int id)
    {
        var imagen = await _context.Imagens.FindAsync(id);
        return imagen?.DatosBase64!;
    }
}

}