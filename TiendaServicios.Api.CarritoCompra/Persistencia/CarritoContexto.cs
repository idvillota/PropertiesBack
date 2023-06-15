using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Modelo;

namespace TiendaServicios.Api.CarritoCompra.Persistencia
{
    public class CarritoContexto: DbContext
    {
        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritosesionDetalle { get; set; }

        public CarritoContexto(DbContextOptions<CarritoContexto> options)
            : base(options)
        {
            
        }
    }
}
