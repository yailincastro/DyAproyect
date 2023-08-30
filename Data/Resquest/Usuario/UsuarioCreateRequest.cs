
using System.ComponentModel.DataAnnotations;

namespace DyAproyect.Data.Resquest.Usuario
{

    public class UsuarioCreateRequest
{
    [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
    public string Nombre { get; set; } = "";
    [Required(ErrorMessage = "El UserName del usuario es obligatorio.")]
    public string UserName { get; set; } = "";
    [Required(ErrorMessage = "La contraseña del usuario es obligatoria."),DataType(DataType.Password), StringLength(8,MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 4 y 8 caracteres.")]
    public string Password { get; set; } = "";
}
}