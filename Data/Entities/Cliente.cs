using DyAproyect.Data.Resquest;
using DyAproyect.Data.Response;
using System.ComponentModel.DataAnnotations;

namespace DyAproyect.Data.Entities
{
     public class Cliente
    {
        [Key]
        
        public int Id {get; set;}
        public string Nombre {get; set;}=null!;
        public string Apellido {get; set;}=null!;
        public string Cedula {get; set;}=null!;


        public static Cliente Crear(ClientesRequest cliente)
        => new Cliente()
        {
            Nombre=cliente.Nombre,
            Apellido=cliente.Apellido,
            Cedula=cliente.Cedula

        };

        public bool Modoficar(ClientesRequest cliente)
        {
            var cambio=false;
            if(Nombre != cliente.Nombre)
            {
                Nombre = cliente.Nombre;
                cambio = true;
            }
            if(Apellido != cliente.Apellido)
            {
                Apellido = cliente.Apellido;
                cambio = true;
            }
            if(Cedula != cliente.Cedula)
            {
                Cedula = cliente.Cedula;
                cambio = true;
            }
            return cambio;

        }
        public ClienteResponse ToResponse()=>new ClienteResponse()
        {
            Id=Id,
            Nombre=Nombre,
            Apellido=Apellido,
            Cedula=Cedula
        };

    }


}