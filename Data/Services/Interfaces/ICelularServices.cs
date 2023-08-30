using DyAproyect.Data.Context;
using DyAproyect.Data.Response;
using DyAproyect.Data.Resquest;

namespace DyAproyect.Data.Services.Interfaces
{

      public interface ICelularServices
    {
        Task<Result> Crear(CelularResquest request);
        Task<Result> Eliminar(int id);
        Task<Result> Modificar(CelularResquest request);
        Task<Result<List<CelularResponse>>> Consultar(string filtro);
        Task<Result<List<CelularResponse>>> ObtenerListaCelulares();


    }
}