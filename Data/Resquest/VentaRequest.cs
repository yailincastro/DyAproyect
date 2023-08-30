using DyAproyect.Data.Entities;

namespace DyAproyect.Data.Resquest
{

      public class VentaRequest
    {
		    public int Id { get; set; }

        public string Nombre {get; set;}=null!;
        public string Apellido {get; set;}=null!;
        public int Cantidad {get; set;}
        public decimal PrecioUnitario{get; set;}
        public decimal Total {get; set;}
    }
}