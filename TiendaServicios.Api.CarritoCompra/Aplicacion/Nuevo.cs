using MediatR;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: IRequest
        {
            public DateTime? Fecha { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador: IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _carritoContexto;

            public Manejador(CarritoContexto carritoContexto)
            {
                _carritoContexto = carritoContexto;
            }

            public async Task Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                    { 
                        FechaCreacion = DateTime.Now 
                    };

                _carritoContexto.CarritoSesion.Add(carritoSesion);
                var value = await _carritoContexto.SaveChangesAsync();

                if (value == 0)
                    throw new Exception("Errores en la insercion del carrito de compras");

                int id = carritoSesion.CarritoSesionId;

                foreach (var item in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = item
                    };

                    _carritoContexto.CarritosesionDetalle.Add(detalleSesion);
                }

                value = await _carritoContexto.SaveChangesAsync();

                if (value == 0)
                    throw new Exception("No se pudo insertar el detalle del carrito de compras");
            }
        }
    }
}
