
using System.ComponentModel.DataAnnotations;

namespace DyAproyect.Data.Resquest
{

    public class LoginResquest
{
    [Required(ErrorMessage = "El usuario es obligatorio.")]
    public string username { get; set; } = "";
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string password { get; set; } = "";
}
  }

