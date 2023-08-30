using DyAproyect.Autenticacion;
using DyAproyect.Data.Context;
using DyAproyect.Data.Entities;
using DyAproyect.Data.extension;
using DyAproyect.Data.Resquest;
using DyAproyect.Data.Resquest.Usuario;
using DyAproyect.Data.Response;
using DyAproyect.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DyAproyect.Data.Services
{
//Se declara el SingleResponse para una respuesta simple de un solo dato...
using SingleResponse = Result<UsuarioResponse>;
using ListResponse = ResultList<UsuarioResponse>;

public class UserManagerService : IUserManagerService
{
    private readonly IDyAproyectDbContext dbContext;
    private readonly IAutenticacion autenticacion;
    private readonly ICurrentUserService currentUserService;

    public UserManagerService(IDyAproyectDbContext dbContext, IAutenticacion autenticacion, ICurrentUserService currentUserService)
    {
        this.dbContext = dbContext;
        this.autenticacion = autenticacion;
        this.currentUserService = currentUserService;
    }
    //El login devuelve una respuesta simple...
    public async Task<SingleResponse> Login(LoginResquest  request)
    {
        try
        {
            //Para los casos fallidos, es este mensaje.
            var sms_fail = "Credenciales incorrectas";
            //Se busca el usuario en la base de datos
            var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.UserName == request.username);
            //Si no se encuentra el usuario, mesaje fallido.
            if (user == null) return SingleResponse.Failed(sms_fail);
            //Si no es la contrasena del usuario, mesaje fallido.
            if (!user.Authorized(request.password)) return SingleResponse.Failed(sms_fail);
            //Si logra superar todas las validaciones devolvemos el loginResponse.
            var model = user.ToResponse();
            await autenticacion.UpdateAuthenticationState(model);
            return SingleResponse.Succesed(model);
        }
        catch (Exception E)
        {
            return SingleResponse.Failed(E.Message);
        }
    }
    public async Task<Result> Logout()
    {
        try
        {

            await autenticacion.UpdateAuthenticationState(null!);
            return Result.Successed("Sesión cerrada exitosamente!");
        }
        catch (Exception E)
        {
            return Result.Failed(E.Message);
        }
    }
    //Crear usuarios basicos...
    public async Task<Result> Create(UsuarioCreateRequest request)
    {
        try
        {
            var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user != null) return Result.Failed($"Ya existe un usuario con el seudonimo '{request.UserName}'.");
            user = Usuario.Crear(request);
            dbContext.Usuarios.Add(user);
            await dbContext.SaveChangesAsync();
            return Result.Successed("Usuario creado exitosamente!");
        }
        catch (Exception E)
        {
            return Result.Failed(E.Message);
        }
    }
    //Modificar usuarios...
    public async Task<SingleResponse> Update(UsuarioRequest request)
    {
        try
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == request.Id);
            if (usuario == null) return SingleResponse.Failed("No fue posible encontrar el usuario indiado...");
            if (usuario.Nombre != request.Nombre)
            {
                usuario.Nombre = request.Nombre;
                await dbContext.SaveChangesAsync();
                var model = usuario.ToResponse();
                await autenticacion.UpdateAuthenticationState(model);
                return SingleResponse.Succesed(model, "Se ha cambiado el nombre del usuario exitosamente...");
            }
            return SingleResponse.Failed("No se realizó, ningún cambio...");
        }
        catch (Exception E)
        {
            return SingleResponse.Failed(E.Message);
        }
    }
    //Cambiar contrasena
    public async Task<SingleResponse> ChangePassword(UsuarioChangePasswordRequest request)
    {
        try
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == request.Id);
            if (usuario == null) return SingleResponse.Failed("No fue posible encontrar el usuario indiado...");
            if (usuario.Authorized(request.OldPassword))
            {
                usuario.Password = request.NewPassword.Encriptar();
                await dbContext.SaveChangesAsync();
                return SingleResponse.Succesed(usuario.ToResponse(), "Se ha cambiado la contraseña exitosamente...");
            }
            return SingleResponse.Failed("Su contraseña no coincide...");

        }
        catch (Exception E)
        {
            return SingleResponse.Failed(E.Message);
        }
    }
    //Cambiar de rol
    public async Task<SingleResponse> ChangeRole(UsuarioChangeRoleRequest request)
    {
        try
        {
            var CurrentUserId = await currentUserService.UserId();
            //Representa el usuario al que se le cambiara el rol.
            var usuario_beneficiado = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == request.IdUserToChange);
            if (usuario_beneficiado == null) return SingleResponse.Failed("No fue posible encontrar el usuario a cambiar...");
            //Representa el usuario que esta solicitanto el cambio de rol.
            var usuario_operador = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == CurrentUserId);
            if (usuario_operador == null) return SingleResponse.Failed("No fue posible encontrar el usuario operador...");
            //Solo si el usuario que solicita el cambio de rol es administrador, se procede a realizar el cambio.
            if (usuario_operador.IsAdmin())
            {
                usuario_beneficiado.Role = request.Role;
                await dbContext.SaveChangesAsync();
                return SingleResponse.Succesed(usuario_beneficiado.ToResponse(), "Se ha cambiado la contraseña exitosamente...");
            }
            //Se retorna fallido si no es administrador.
            return SingleResponse.Failed("Usted no está autorizado para realizar el cambio de rol.");

        }
        catch (Exception E)
        {
            return SingleResponse.Failed(E.Message);
        }
    }
    //Listar los usuarios existentes
    public async Task<ListResponse> GetAsync()
    {
        try
        {
            var users = (await dbContext.Usuarios.ToListAsync())
                .Select(user => user.ToResponse())
                .ToList();
            if (users == null) return ListResponse.Failed("No hay usuarios registrados...");

            return ListResponse.Successed(users);
        }
        catch (Exception e)
        {
            return ListResponse.Failed(e.Message);
        }
    }
    //Listar los usuarios existentes
    public async Task<Result> Delete(int Id)
    {
        try
        {
            var CurrentUserId = await currentUserService.UserId();
            //Representa el usuario al que se le cambiara el rol.
            var user_to_delete = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == Id);
            if (user_to_delete == null) return SingleResponse.Failed("No fue posible encontrar el usuario a cambiar...");
            //Representa el usuario que esta solicitanto el cambio de rol.
            var usuario_operador = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == CurrentUserId);
            if (usuario_operador == null) return SingleResponse.Failed("No fue posible encontrar el usuario operador...");
            //Solo si el usuario que solicita el cambio de rol es administrador, se procede a realizar el cambio.
            if (usuario_operador.IsAdmin())
            {
                dbContext.Usuarios.Remove(user_to_delete);
                await dbContext.SaveChangesAsync();
                return SingleResponse.Succesed(user_to_delete.ToResponse(), "Se ha eliminado el usuario exitosamente...");
            }
            //Se retorna fallido si no es administrador.
            return SingleResponse.Failed("Usted no está autorizado para eliminar un usuario.");

        }
        catch (Exception e)
        {
            return ListResponse.Failed(e.Message);
        }
    }
}


    public interface IUserManagerService
    {
        Task<Result<UsuarioResponse>> ChangePassword(UsuarioChangePasswordRequest request);
        Task<Result<UsuarioResponse>> ChangeRole(UsuarioChangeRoleRequest request);
        Task<Result> Create(UsuarioCreateRequest request);
        Task<ResultList<UsuarioResponse>> GetAsync();
        Task<Result<UsuarioResponse>> Login(LoginResquest request);
        Task<Result> Logout();
        Task<Result<UsuarioResponse>> Update(UsuarioRequest request);
        Task<Result> Delete(int Id);
    }
}

