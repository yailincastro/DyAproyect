
using Microsoft.EntityFrameworkCore;
using DyAproyect.Data.Entities;
using System.Collections.Generic;

namespace DyAproyect.Data.Context
{
    public interface IDyAproyectDbContext
    {
        
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Celular> Celulares{get; set;}
        public DbSet<Accesorio> Accesorios {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Imagen> Imagens {get; set;}
        public DbSet<Venta> Ventas {get; set;}

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

   
}
