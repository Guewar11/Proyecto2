using Microsoft.EntityFrameworkCore;
using Proyecto2_Api.Modelo;

namespace Proyecto2_Api.Datos
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<ProyectoModelo> ProyectoModelos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProyectoModelo>().HasData(
                new ProyectoModelo()
                {
                    Id = 1,
                    Nombre = "Vista Panorámica",
                    Detalle = "Detalle de la Vista..",
                    ImagenUrl = "",
                    Cantidades = 5,
                    MetrosCuadrados = 90,
                    Tarifa = 1500,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new ProyectoModelo()
                {
                    Id = 2,
                    Nombre = "Vista Premiun Panorámica",
                    Detalle = "Detalle de la Vista..",
                    ImagenUrl = "",
                    Cantidades = 1,
                    MetrosCuadrados = 140,
                    Tarifa = 5800,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
           );
        }
    }
}
