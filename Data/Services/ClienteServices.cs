using DyAproyect.Data.Context;
using DyAproyect.Data.Entities;
using DyAproyect.Data.Resquest;
using DyAproyect.Data.Response;
using Microsoft.EntityFrameworkCore;
using DyAproyect.Data.Services.Interfaces;

namespace DyAproyect.Data.Services
{
     public class ClienteServices : IClienteServices
    {
        private readonly IDyAproyectDbContext dbContext;

        public ClienteServices(IDyAproyectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Método crear
        public async Task<Result> Crear(ClientesRequest request)
        {
            try
            {
                var cliente = Cliente.Crear(request);
                dbContext.Clientes.Add(cliente);
                await dbContext.SaveChangesAsync();
                return new Result() { Message = "ok", Success = true };
            }
            catch (Exception e)
            {
                return new Result() { Message = e.Message, Success = false };
            }
        }

        // Método Modificar
        public async Task<Result> Modificar(ClientesRequest request)
        {
            try
            {
                var cliente = await dbContext.Clientes.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (cliente == null)
                    return new Result() { Message = "No se encontró el cliente", Success = false };

                if (cliente.Modoficar(request))
                    await dbContext.SaveChangesAsync();

                return new Result() { Message = "Ok", Success = true };
            }
            catch (Exception e)
            {
                return new Result() { Message = e.Message, Success = false };
            }
        }

        // Método Eliminar
        public async Task<Result> Eliminar(ClientesRequest request)
        {
            try
            {
                var cliente = await dbContext.Clientes.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (cliente == null)
                    return new Result() { Message = "No se encontró el cliente", Success = false };

                dbContext.Clientes.Remove(cliente);
                await dbContext.SaveChangesAsync();

                return new Result() { Message = "Ok", Success = true };
            }
            catch (Exception e)
            {
                return new Result() { Message = e.Message, Success = false };
            }
        }

        // Método Listar o consultar
        public async Task<Result<List<ClienteResponse>>> Consultar(string filtro)
        {
            try
            {
                var clientes = await dbContext.Clientes
                    .Where(c =>
                        (c.Nombre + " " + c.Apellido +""+ c.Cedula)
                        .ToLower()
                        .Contains(filtro.ToLower())
                    )
                    .Select(c => c.ToResponse())
                    .ToListAsync();

                return new Result<List<ClienteResponse>>()
                {
                    Message = "Ok",
                    Success = true,
                    Data = clientes
                };
            }
            catch (Exception e)
            {
                return new Result<List<ClienteResponse>>()
                {
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }

    
}


