using DyAproyect.Data.Response;
using DyAproyect.Data.Resquest;

namespace DyAproyect.Data.Services.Interfaces
{
        public interface IVentaServices
    {
        Task<Result<List<VentaResponse>>> Consultar(string filtro);
        Task<Result> Crear(VentaRequest request);
        Task<Result> Eliminar(VentaRequest request);
        Task<Result> Modificar(VentaRequest request);
    }

}