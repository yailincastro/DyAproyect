using Microsoft.EntityFrameworkCore;
using DyAproyect.Data.Entities;
using System.Collections.Generic;

namespace DyAproyect.Data.Context

{
    public class DyAproyectDbContext : DbContext, IDyAproyectDbContext
    {
        private readonly IConfiguration config;

        public DyAproyectDbContext(IConfiguration config) 
        {
            this.config = config;
        }
        
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Celular> Celulares {get; set;}
        public DbSet<Accesorio> Accesorios {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
      
        public DbSet<Imagen> Imagens {get; set;}
        public DbSet<Venta> Ventas {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: config.GetConnectionString(name: "MSSQL")); // En esta línea se configura el proveedor de base de datos y la cadena de conexión
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
