using DyAproyect.Data.Context;
using DyAproyect.Data.Entities;
using DyAproyect.Data.Resquest;
using DyAproyect.Data.Response;
using Microsoft.EntityFrameworkCore;
using DyAproyect.Data.Services.Interfaces;

namespace DyAproyect.Data.Services
{
    
    public class AccesorioServices: IAccesorioServices

{
        private readonly IDyAproyectDbContext _dbContext;

        public AccesorioServices(IDyAproyectDbContext dbContext)
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

        public async Task<Result> Crear(AccesorioResquest request)
        {
            try
            {
                _dbContext.Accesorios.Add(Accesorio.Crear(request));
                await _dbContext.SaveChangesAsync();
                return new Result() { Message = "Accesorio creado exitosamente.", Success = true };
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
                var accesorio = await _dbContext.Accesorios.FirstOrDefaultAsync(c => c.Id == id);
                if (accesorio != null)
                {
                    _dbContext.Accesorios.Remove(accesorio);
                    await _dbContext.SaveChangesAsync();
                }
                return new Result() { Message = "accesorio eliminado exitosamente.", Success = true };
            }
            catch (Exception ex)
            {
                return new Result() { Message = ex.Message, Success = false };
            }
        }

        public async Task<Result> Modificar(AccesorioResquest request)
        {
            try
            {
                var accesorio = await _dbContext.Accesorios.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (accesorio == null)
                    return new Result() { Message = "accesorio no encontrado.", Success = false };

                accesorio.Modoficar(request);
                await _dbContext.SaveChangesAsync();
                return new Result() { Message = "accesorio modificado exitosamente.", Success = true };
            }
            catch (Exception ex)
            {
                return new Result() { Message = ex.Message, Success = false };
            }
        }

        public async Task<Result<List<AccesorioResponse>>> Consultar(string filtro)
        {
            try
            {
                var accesorios = await _dbContext.Accesorios.ToListAsync();
                var accesoriosFiltrados = accesorios
                    .Where(c => (c.Nombre + " " + c.Descripcion + " " + c.Imagen + " " + c.Precio + " " + c.Cantidad)
                    .ToLower()
                    .Contains(filtro.ToLower()))
                    .Select(c => c.ToResponse())
                    .ToList();

                return new Result<List<AccesorioResponse>>()
                {
                    Message = "Consulta exitosa.",
                    Success = true,
                    Data = accesoriosFiltrados
                };
            }
            catch (Exception ex)
            {
                return new Result<List<AccesorioResponse>>()
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<List<AccesorioResponse>>> ObtenerListaCelulares()
        {
            try
            {
                var accesorios = await _dbContext.Accesorios.ToListAsync();
                var accesorioResponse = accesorios.Select(c => c.ToResponse()).ToList();

                return new Result<List<AccesorioResponse>>()
                {
                    Message = "Lista de celulares obtenida exitosamente.",
                    Success = true,
                    Data = accesorioResponse
                };
            }
            catch (Exception ex)
            {
                return new Result<List<AccesorioResponse>>()
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }

    }
}
