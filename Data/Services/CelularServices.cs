using DyAproyect.Data.Context;
using DyAproyect.Data.Entities;
using DyAproyect.Data.Resquest;
using DyAproyect.Data.Response;
using Microsoft.EntityFrameworkCore;
using DyAproyect.Data.Services.Interfaces;

namespace DyAproyect.Data.Services
{

        public class CelularServices: ICelularServices

{
        private readonly IDyAproyectDbContext _dbContext;

        public CelularServices(IDyAproyectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> InicializarBaseDeDatos()
        {
            try
            {
                await DyAproyectDbContextSeeder.Inicializar((DyAproyectDbContext)_dbContext);
                return new Result() { Message = "Base de datos inicializada exitosamente.", Success = true };
            }
            catch (Exception ex)
            {
                return new Result() { Message = ex.Message, Success = false };
            }
        }

        // Métodos CRUD

        public async Task<Result> Crear(CelularResquest request)
        {
            try
            {
                _dbContext.Celulares.Add(Celular.Crear(request));
                await _dbContext.SaveChangesAsync();
                return new Result() { Message = "Celular creado exitosamente.", Success = true };
            }
            catch (Exception ex)
            {
                return new Result() { Message = ex.Message, Success = false };
            }
        }

        public async Task<Result> Eliminar(int id)
        {
            try
            {
                var celular = await _dbContext.Celulares.FirstOrDefaultAsync(c => c.Id == id);
                if (celular != null)
                {
                    _dbContext.Celulares.Remove(celular);
                    await _dbContext.SaveChangesAsync();
                }
                return new Result() { Message = "Celular eliminado exitosamente.", Success = true };
            }
            catch (Exception ex)
            {
                return new Result() { Message = ex.Message, Success = false };
            }
        }

        public async Task<Result> Modificar(CelularResquest request)
        {
            try
            {
                var celular = await _dbContext.Celulares.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (celular == null)
                    return new Result() { Message = "Celular no encontrado.", Success = false };

                celular.Modoficar(request);
                await _dbContext.SaveChangesAsync();
                return new Result() { Message = "Celular modificado exitosamente.", Success = true };
            }
            catch (Exception ex)
            {
                return new Result() { Message = ex.Message, Success = false };
            }
        }

        public async Task<Result<List<CelularResponse>>> Consultar(string filtro)
        {
            try
            {
                var celulares = await _dbContext.Celulares.ToListAsync();
                var celularesFiltrados = celulares
                    .Where(c => (c.Nombre + " " + c.Descripcion + " " + c.Imagen + " " + c.Precio + " " + c.Cantidad)
                    .ToLower()
                    .Contains(filtro.ToLower()))
                    .Select(c => c.ToResponse())
                    .ToList();

                return new Result<List<CelularResponse>>()
                {
                    Message = "Consulta exitosa.",
                    Success = true,
                    Data = celularesFiltrados
                };
            }
            catch (Exception ex)
            {
                return new Result<List<CelularResponse>>()
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<List<CelularResponse>>> ObtenerListaCelulares()
        {
            try
            {
                var celulares = await _dbContext.Celulares.Select(c => c.ToResponse()).ToListAsync();

                return new Result<List<CelularResponse>>()
                {
                    Message = "Lista de celulares obtenida exitosamente.",
                    Success = true,
                    Data = celulares
                };
            }
            catch (Exception ex)
            {
                return new Result<List<CelularResponse>>()
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }


}
}