using DyAproyect.Data.extension;
using Microsoft.VisualBasic;

namespace DyAproyect.Data.Response
{
    public class UsuarioResponse
    {
        
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Role {get; set; } = Constant.Roles.Admin;
     public bool IsAdmin() => Role == Constant.Roles.Admin;
    }


    
}    

