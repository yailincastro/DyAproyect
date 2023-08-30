using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DyAproyect.Data;
using DyAproyect.Data.Context;
using Microsoft.EntityFrameworkCore;
using DyAproyect.Data.Services;
using DyAproyect.Data.Services.Interfaces;
using DyAproyect.Autenticacion;
using Microsoft.AspNetCore.Components.Authorization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthenticationCore();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<DyAproyectDbContext>();
builder.Services.AddScoped<AuthenticationStateProvider,Autenticacion>();
builder.Services.AddScoped<IDyAproyectDbContext,DyAproyectDbContext>();
builder.Services.AddScoped<IAutenticacion, Autenticacion>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();
builder.Services.AddScoped<IClienteServices,ClienteServices>();
builder.Services.AddScoped<IVentaServices,VentaServices>();
builder.Services.AddScoped<IAccesorioServices,AccesorioServices>();
builder.Services.AddScoped<ICelularServices,CelularServices>();
builder.Services.AddScoped<IImagenService, ImagenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var serviceScope= app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{

    var dbContext = serviceScope.ServiceProvider
   .GetRequiredService<DyAproyectDbContext>();
    dbContext.Database.Migrate();
    await DyAproyectDbContextSeeder.Inicializar( dbContext);
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
