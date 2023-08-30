
using DyAproyect.Data.Resquest;

namespace DyAproyect.Data.Response
{
     public class CelularResponse
    {
        
        public int Id {get; set;}

        public string Nombre {get; set;}=null!;

        public string Descripcion {get; set;}=null!;

        public string Imagen {get; set;}=null!;

        public decimal Precio {get; set;}

        public int Cantidad {get; set;}

        public decimal Total => Cantidad * Precio;

        public CelularResquest ToResquest()
        { return new CelularResquest
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


}