using DyAproyect.Data.Context;
using DyAproyect.Data.Response;
using DyAproyect.Data.Resquest;

namespace DyAproyect.Data.Services.Interfaces
{

    public interface IAccesorioServices
    {
            
        Task<Result> Crear(AccesorioResquest request);
        Task<Result> Eliminar(int id);
        Task<Result> Modificar(AccesorioResquest request);
        Task<Result<List<AccesorioResponse>>> Consultar(string filtro);
        Task<Result<List<AccesorioResponse>>> ObtenerListaCelulares();

    }
}