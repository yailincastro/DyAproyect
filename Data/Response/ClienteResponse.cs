using DyAproyect.Data.Resquest;

namespace DyAproyect.Data.Response
{
     public class ClienteResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        
        public string Apellido { get; set;}= null!;

        public string Cedula  {get; set;}=null!;

        public ClientesRequest ToRequest() {
            return new ClientesRequest
            {
                Id = Id,
                Nombre = Nombre,
                Apellido = Apellido,
                Cedula = Cedula

            };
        }
    }


}