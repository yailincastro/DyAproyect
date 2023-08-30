using DyAproyect.Data.Constant;
namespace DyAproyect.Data.Resquest.Usuario
{

 public class UsuarioChangeRoleRequest
{
    public int IdUserToChange { get; set; }
    public int IdCurrentUser { get; set; }
    public string Role { get; set; } = Roles.Basic;
}
}