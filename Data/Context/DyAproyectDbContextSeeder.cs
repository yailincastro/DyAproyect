using System.Collections;
using DyAproyect.Data.Context;
using DyAproyect.Data.Entities;
using DyAproyect.Data.Constant;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace DyAproyect.Data.Context
{

    public class DyAproyectDbContextSeeder
    {   
         public static async Task Inicializar(DyAproyectDbContext context)
        {
            await AddCelulares(context);
            await AddAccesorios(context);
            await GenerarUsuarioAdmin(context);
            await GenerarUsuarioInvintado(context);
            await GenerarUsuarioEmpleado(context);
            await context.SaveChangesAsync();
        }
        private static async Task AddCelulares(DyAproyectDbContext context){
            if(!context.Celulares.Any()){
            var celulares = new List<Celular>
            {
                new Celular { Nombre = "IPHONE 14 PRO MAX",Descripcion= "a", Precio = 80000.00m, Imagen = "img/iphone14.png",Cantidad=0 },
                new Celular { Nombre = "Samsung S23 Ultra", Descripcion="1", Precio = 60000.00m, Imagen = "img/s23.jfif",Cantidad=0 },
                new Celular { Nombre = "Ipro a6 mini",Descripcion="2", Precio = 799.99m, Imagen = "img/ipro-A6-mini.png", Cantidad=0},
                new Celular { Nombre = "Realme 12 pro",Descripcion= "3", Precio = 20000.99m, Imagen = "img/realme.png", Cantidad=0},
                new Celular { Nombre = "Redmi note 13 Pro",Descripcion="4", Precio = 30000.00m, Imagen = "img/redminote13.pgn.jfif",Cantidad=0 },
                new Celular { Nombre = "Samsung 22 Ultra",Descripcion="5", Precio = 39999.99m, Imagen = "img/22s.jfif",Cantidad=0 },
                new Celular { Nombre = "Poco x3",Descripcion="6", Precio = 14999.99m, Imagen = "img/poco.jfif",Cantidad=0 },
                new Celular { Nombre = "fire 8 tablet amazon",Descripcion="7", Precio = 4999.99m, Imagen = "img/amazojfif.jfif",Cantidad=0 },
            };
            context.AddRange(celulares);
            await context.SaveChangesAsync();
            }
        }

             private static async Task AddAccesorios(DyAproyectDbContext context){
            if(!context.Accesorios.Any()){
            var accesorios = new List<Accesorio>
            {
                new Accesorio { Nombre = "Auriculare",Descripcion="a", Precio = 800.00m, Imagen = "img/imagen.png",Cantidad=0 },
                new Accesorio { Nombre = "Cover de IPhone",Descripcion="1", Precio = 300.00m, Imagen = "img/ipho.png",Cantidad=0},
                new Accesorio{ Nombre = "auriculare a bluetooth",Descripcion="2", Precio = 950.49m, Imagen = "img/auriculare.jpg",Cantidad=0 },
                new Accesorio{ Nombre = "auriculares a bluetooth",Descripcion="3", Precio = 899.99m, Imagen = "img/auriculare.jfif",Cantidad=0 },
                new Accesorio{ Nombre = "Pro",Descripcion="4", Precio = 1999.99m, Imagen = "img/audifonos-bluetooth.jpg", Cantidad=0},
                new Accesorio{ Nombre = "cover de airp",Descripcion="5", Precio = 249.99m, Imagen = "img/cover.webp",Cantidad=0},
                new Accesorio{ Nombre = "cover de tablet amazon 7",Descripcion="6", Precio = 349.99m, Imagen = "img/coveramazojpg.jpg",Cantidad=0 },
                new Accesorio{ Nombre = "cover de ariculares",Descripcion="7", Precio = 249.99m, Imagen = "img/coverdeaudifono.jfif",Cantidad=0 },
                new Accesorio { Nombre = "correa de reloj",Descripcion="8", Precio = 249.99m, Imagen = "img/gif-correas-goma.gif",Cantidad=0 },
                new Accesorio{ Nombre = "cover de ariculares",Descripcion="9", Precio = 249.99m, Imagen = "img/coverdeaudifono.jfif",Cantidad=0 },
                new Accesorio { Nombre = "reloj inteligente",Descripcion="10", Precio = 949.99m, Imagen = "img/reloj.jpg",Cantidad=0 },
                new Accesorio { Nombre = "reloj imteligente",Descripcion="11", Precio = 849.99m, Imagen = "img/relo.jpg",Cantidad=0 },
            };
            context.AddRange(accesorios);
            await context.SaveChangesAsync();
            }

               }

              private static async Task<bool> GenerarUsuarioAdmin(DyAproyectDbContext context)
              {
                         var role = Constant.Roles.Admin;
                         var admin = await context.Usuarios.FirstOrDefaultAsync(user => user.Role == role);
                     if (admin==null)
                     {
                       admin = Usuario.Crear("El Shini ","elshini","1234", role);
                       context.Usuarios.Add(admin);
                           return true;//Forzar un safe change unico...
                         }
                         return false;//Evitar un safe change unico...
                        
               }

                private static async Task<bool> GenerarUsuarioEmpleado(DyAproyectDbContext context)
              {
                         var role = Constant.Roles.Empleado1;
                         var empleado = await context.Usuarios.FirstOrDefaultAsync(user => user.Role == role);
                     if (empleado==null)
                     {
                       empleado = Usuario.Crear("empleado 1 ","empleadoa","1234", role);
                       context.Usuarios.Add(empleado);
                           return true;//Forzar un safe change unico...
                         }
                         return false;//Evitar un safe change unico...
                        
               }

               private static async Task<bool> GenerarUsuarioInvintado(DyAproyectDbContext context)
              {
                         var role = Constant.Roles.Invitado;
                         var invitado = await context.Usuarios.FirstOrDefaultAsync(user => user.Role == role);
                     if (invitado==null)
                     {
                       invitado = Usuario.Crear("ayi tu real burra ","ayilaburra","1234", role);
                       context.Usuarios.Add(invitado);
                           return true;//Forzar un safe change unico...
                         }
                         return false;//Evitar un safe change unico...
                        
               }
    
    }
}