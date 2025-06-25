using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Reclamo.Application.Commands;
using Reclamo.Domain.Aggregates;
using Reclamo.Domain.Events;
using Reclamo.Domain.Repositories;
using Reclamo.Domain.ValueObjects;

namespace Reclamo.Application.Handlers
{
    /// <summary>
    /// Manejador para el comando de creación de reclamos.
    /// Se encarga de crear el reclamo y publicar el evento correspondiente.
    /// </summary>
    public class CreateReclamoCommandHandler : IRequestHandler<CreateReclamoCommand, string>
    {
        private readonly IReclamoRepository _reclamoRepository;
        private readonly IMediator _mediator;

        public CreateReclamoCommandHandler(
            IReclamoRepository reclamoRepository,
            IMediator mediator)
        {
            _reclamoRepository = reclamoRepository ?? throw new ArgumentNullException(nameof(reclamoRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<string> Handle(CreateReclamoCommand request, CancellationToken cancellationToken)
        {
            var reclamoId = new ReclamoId(Guid.NewGuid().ToString());

            var reclamo = new Aggregate_Reclamo(
                reclamoId,
                request.UsuarioId,
                request.SubastaId,
                new Motivo(request.Motivo),
                new Descripcion(request.Descripcion),
                string.IsNullOrEmpty(request.EvidenciaUrl) ? null : new EvidenciaUrl(request.EvidenciaUrl),
                EstadoReclamo.Pendiente,
                null
            );

            await _reclamoRepository.AddAsync(reclamo);

            var reclamoCreadoEvent = new ReclamoCreadoEvent(
                reclamo.Id.Value,
                reclamo.UsuarioId.Value,
                reclamo.SubastaId?.Value,
                reclamo.Motivo.Value,
                reclamo.Descripcion.Value,
                reclamo.EvidenciaUrl?.Value,
                reclamo.Estado.ToString(),
                null,
                reclamo.FechaCreacion
            );

            await _mediator.Publish(reclamoCreadoEvent, cancellationToken);

            return reclamo.Id.Value;
        }
    }
}