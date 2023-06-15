using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ContextoAutor _contextoAutor;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor contextoAutor, IMapper mapper)
            {
                _contextoAutor = contextoAutor;
                _mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autorLibro = await _contextoAutor.AutorLibro.FirstOrDefaultAsync (a => a.AutorLibroGuid == request.AutorGuid);

                if (autorLibro == null)
                    throw new Exception("No se encontro el autor");

                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autorLibro);

                return autorDto;
            }
        }

    }
}
