using DyAproyect.Data.Resquest;

namespace DyAproyect.Data.Response{

     public class VentaResponse
    {
        public int Id { get; set; }
		public string Nombre {get; set;}=null!;
		public string Apellido {get; set;}=null!;
		public int Cantidad {get; set;}
		public decimal PrecioUnitario{get;set;}
		public decimal Total {get;set;}
        
		public VentaRequest ToRequest()
		{
			return new VentaRequest
			{
				Id = Id,
				Nombre=Nombre,
				Apellido=Apellido,
				Cantidad=Cantidad,
				PrecioUnitario=PrecioUnitario,
				Total=Total
			};
		}
	}
}

