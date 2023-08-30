
using System.ComponentModel.DataAnnotations;
using DyAproyect.Data.extension;
using DyAproyect.Data.Constant;
using DyAproyect.Data.Response;
using DyAproyect.Data.Resquest.Usuario;

namespace DyAproyect.Data.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
       public string Nombre {get; set;}=null!;
       public string UserName {get; set;}=null!;

       public string Password {get; set;}=null!;

       public string Role {get; set;}= Roles.Basic;
       public static Usuario Crear(UsuarioCreateRequest request) =>
        new(request.Nombre, request.UserName, request.Password);
         public static Usuario Crear(string nombre, string username,string password,string role)
         =>new (nombre,username,password,role);
     public bool Authorized(string password)
    {
        return Password == password.Encriptar();
    }
    public Usuario()
    {
        Role= Roles.Basic;
        Role= Roles.Empleado1;

    }
    public Usuario(string nombre, string username,string password, string role)
    {

        Nombre= nombre;
        UserName=username;
        Password=password.Encriptar();
        Role=role;
       

    }

        public Usuario(string nombre, string userName, string password)
        {
            Nombre = nombre;
            UserName = userName;
            Password = password;
            Role=Roles.Basic;
        }

      
        public UsuarioResponse ToResponse()
        => new()
        {
            Id=Id,
            Nombre=Nombre,
            UserName=UserName,
            Role=Role

        };

     internal bool IsAdmin()
    {
        return Role == Constant.Roles.Admin;
    }
    }
    
}