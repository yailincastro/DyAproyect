using DyAproyect.Data.Context;
using DyAproyect.Data.Entities;
using DyAproyect.Data.Resquest;
using DyAproyect.Data.Response;
using Microsoft.EntityFrameworkCore;
using DyAproyect.Data.Services.Interfaces;

namespace DyAproyect.Data.Services
{
public class VentaServices : IVentaServices
    {
        private readonly IDyAproyectDbContext _dbContext;

        public VentaServices(IDyAproyectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método CrearVenta
          public async Task<Result> Crear(VentaRequest request)
        {
            try
            {
                var venta = Venta.Crear(request);
                _dbContext.Ventas.Add(venta);
                await _dbContext.SaveChangesAsync();
                return new Result() { Message = "ok", Success = true };
            }
            catch (Exception e)
            {
                return new Result() { Message = e.Message, Success = false };
            }
        }

        // Método Modificar
        public async Task<Result> Modificar(VentaRequest request)
        {
            try
            {
                var venta = await _dbContext.Ventas.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (venta == null)
                    return new Result() { Message = "No se encontró el cliente", Success = false };

                if (venta.Modoficar(request))
                    await _dbContext.SaveChangesAsync();

                return new Result() { Message = "Ok", Success = true };
            }
            catch (Exception e)
            {
                return new Result() { Message = e.Message, Success = false };
            }
        }

        // Método Eliminar
        public async Task<Result> Eliminar(VentaRequest request)
        {
            try
            {
                var venta = await _dbContext.Ventas.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (venta == null)
                    return new Result() { Message = "No se encontró el cliente", Success = false };

                _dbContext.Ventas.Remove(venta);
                await _dbContext.SaveChangesAsync();

                return new Result() { Message = "Ok", Success = true };
            }
            catch (Exception e)
            {
                return new Result() { Message = e.Message, Success = false };
            }
        }

        // Método Listar o consultar
        public async Task<Result<List<VentaResponse>>> Consultar(string filtro)
        {
            try
            {
                var ventas = await _dbContext.Ventas
                    .Where(c =>
                        (c.Nombre + " " + c.Apellido +""+ c.Cantidad +""+ c.PrecioUnitario +""+c.Total)
                        .ToLower()
                        .Contains(filtro.ToLower())
                    )
                    .Select(c => c.ToResponse())
                    .ToListAsync();

                return new Result<List<VentaResponse>>()
                {
                    Message = "Ok",
                    Success = true,
                    Data = ventas
                };
            }
            catch (Exception e)
            {
                return new Result<List<VentaResponse>>()
                {
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }

    
}


