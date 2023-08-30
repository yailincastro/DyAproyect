using DyAproyect.Data.Resquest;
using DyAproyect.Data.Response;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DyAproyect.Data.Entities
{

      public class Venta
    {
        [Key]
        public int Id { get; set; }

        public string Nombre {get; set;}=null!;

        public string Apellido {get; set;}=null!;

        public int Cantidad {get; set;}
        
        public decimal PrecioUnitario {get;set;}

        public decimal Total {get; set;}

         public static Venta Crear(VentaRequest venta)
        => new Venta()
        {
            Nombre=venta.Nombre,
            Apellido=venta.Apellido,
            Total=venta.Total,
            PrecioUnitario=venta.PrecioUnitario,
            Cantidad=venta.Cantidad

        };
             public bool Modoficar(VentaRequest venta)
        {
            var cambio=false;
            if(Nombre != venta.Nombre)
            {
                Nombre = venta.Nombre;
                cambio = true;
            }
            if(Apellido != venta.Apellido)
            {
                Apellido = venta.Apellido;
                cambio = true;
            }
            if(Cantidad != venta.Cantidad)
            {
                Cantidad = venta.Cantidad;
                cambio = true;
            }

            if(PrecioUnitario != venta.PrecioUnitario)
            {
                PrecioUnitario = venta.PrecioUnitario;
                cambio = true;
            }

              if(PrecioUnitario != venta.PrecioUnitario)
            {
                PrecioUnitario = venta.PrecioUnitario;
                cambio = true;
            }
              if(Total != venta.Total)
            {
                    Total= venta.Total;
                cambio = true;
            }
            return cambio;
        }


         public VentaResponse ToResponse()=> new VentaResponse()
        {
            Id=Id,
            Nombre=Nombre,
            Apellido=Apellido,
            Total=Total,
            PrecioUnitario=PrecioUnitario,
            Cantidad=Cantidad

        };



    }

    }