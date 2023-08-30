using DyAproyect.Data.Context;
using DyAproyect.Data.Response;
using DyAproyect.Data.Resquest;

namespace DyAproyect.Data.Services.Interfaces
{

     public interface IClienteServices
    {
        
        Task<Result<List<ClienteResponse>>> Consultar(string filtro);
        Task<Result> Crear(ClientesRequest request);
        Task<Result> Eliminar(ClientesRequest request);
        Task<Result> Modificar(ClientesRequest request);


    }
}