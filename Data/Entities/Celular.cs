using DyAproyect.Data.Resquest;
using DyAproyect.Data.Response;
using System.ComponentModel.DataAnnotations;

namespace DyAproyect.Data.Entities
{
  public class Celular
    {
        [Key]

        public int Id {get; set;}

        public string Nombre {get; set;}=null!;

        public string Descripcion {get; set;}=null!;

        public string Imagen {get; set;}=null!;

        public decimal Precio {get; set;}

        public int Cantidad {get; set;}


        public static Celular Crear(CelularResquest celular)
        => new()
        {
            Nombre=celular.Nombre,
            Descripcion=celular.Descripcion,
            Imagen=celular.Imagen,
            Precio=celular.Precio,
            Cantidad=celular.Cantidad

        };

        public bool Modoficar(CelularResquest celular)
        {
            var cambio=false;
            if(Nombre != celular.Nombre)
            {
                Nombre = celular.Nombre;
                cambio = true;
            }
             if(Descripcion != celular.Descripcion)
            {
                Descripcion = celular.Descripcion;
                cambio = true;
            }
             if(Imagen != celular.Imagen)
            {
                Imagen = celular.Imagen;
                cambio = true;
            }
             if(Precio != celular.Precio)
            {
                Precio = celular.Precio;
                cambio = true;
            }
             if(Cantidad != celular.Cantidad)
            {
                Cantidad = celular.Cantidad;
                cambio = true;
            }
            
            return cambio;

        }
        public CelularResponse ToResponse()=> new CelularResponse()
        {
            Id=Id,
            Nombre=Nombre,
            Descripcion=Descripcion,
            Imagen=Imagen,
            Precio=Precio,
            Cantidad=Cantidad

        };

    }

}