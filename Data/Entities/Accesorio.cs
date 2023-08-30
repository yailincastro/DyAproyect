
using DyAproyect.Data.Resquest;
using DyAproyect.Data.Response;
using System.ComponentModel.DataAnnotations;

namespace DyAproyect.Data.Entities
{
    
    public class Accesorio
    {
        [Key]

        public int Id {get; set;}

        public string Nombre {get; set;}=null!;

        public string Descripcion {get; set;}=null!;

        public string Imagen {get; set;}=null!;

        public decimal Precio {get; set;}

        public int Cantidad {get; set;}

        public static Accesorio Crear(AccesorioResquest accesorio)
        => new()
        {
            Nombre=accesorio.Nombre,
            Descripcion=accesorio.Descripcion,
            Imagen=accesorio.Imagen,
            Precio=accesorio.Precio,
            Cantidad=accesorio.Cantidad

        };

        public bool Modoficar( AccesorioResquest accesorio)
        {

         var cambio=false;
            if(Nombre != accesorio.Nombre)
            {
                Nombre = accesorio.Nombre;
                cambio = true;
            }
             if(Descripcion != accesorio.Descripcion)
            {
                Descripcion = accesorio.Descripcion;
                cambio = true;
            }
             if(Imagen != accesorio.Imagen)
            {
                Imagen = accesorio.Imagen;
                cambio = true;
            }
             if(Precio != accesorio.Precio)
            {
                Precio = accesorio.Precio;
                cambio = true;
            }
             if(Cantidad != accesorio.Cantidad)
            {
                Cantidad = accesorio.Cantidad;
                cambio = true;
            }
            return cambio;

        }
        public AccesorioResponse ToResponse()=> new()
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