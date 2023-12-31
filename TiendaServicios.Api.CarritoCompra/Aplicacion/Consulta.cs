﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto _contexto;
            private readonly ILibrosService _librosService;

            public Manejador(CarritoContexto contexto, ILibrosService librosService)
            {
                _contexto = contexto;
                _librosService = librosService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(c => c.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _contexto.CarritosesionDetalle.Where(c => c.CarritoSesionId == request.CarritoSesionId).ToListAsync();
                var listaCarritoDto = new List<CarritoDetalleDto>();

                foreach (var libro in carritoSesionDetalle)
                {
                    var response = await _librosService.GetLibro(new Guid(libro.ProductoSeleccionado));

                    if (response.resultado)
                    {
                        var objectoLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDto
                        {
                             TituloLibro = objectoLibro.Titulo,
                             FechaPublicacion = objectoLibro.FechaPublicacion,
                             LibroId = objectoLibro.LibreriaMaterialId
                        };

                        listaCarritoDto.Add(carritoDetalle);
                    }
                }

                var carritoSesionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDto
                };

                return carritoSesionDto;
            }
        }
    }
}
