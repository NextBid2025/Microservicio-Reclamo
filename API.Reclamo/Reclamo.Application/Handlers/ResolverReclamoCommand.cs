
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Reclamo.Application.Commands;
using Reclamo.Domain.Repositories;
using Reclamo.Domain.ValueObjects;
using Reclamo.Domain.Events;

namespace Reclamo.Application.Handlers
{
    public class ResolverReclamoCommandHandler : IRequestHandler<ResolverReclamoCommand, Unit>
    {
        private readonly IReclamoRepository _reclamoRepository;
        private readonly IMediator _mediator;

        public ResolverReclamoCommandHandler(
            IReclamoRepository reclamoRepository,
            IMediator mediator)
        {
            _reclamoRepository = reclamoRepository ?? throw new ArgumentNullException(nameof(reclamoRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Unit> Handle(ResolverReclamoCommand request, CancellationToken cancellationToken)
        {
            var reclamoId = new ReclamoId(request.ReclamoId);
            var resolucion = new Resolucion(request.Resolucion);
            var estadoFinal = new EstadoReclamo(request.EstadoFinal);

            var reclamo = await _reclamoRepository.GetByIdAsync(reclamoId);
            if (reclamo == null)
                throw new InvalidOperationException("Reclamo no encontrado.");

            // Asegúrate que la firma sea Resolver(Resolucion, EstadoReclamo)
            reclamo.Resolver(resolucion, estadoFinal);
            await _reclamoRepository.UpdateAsync(reclamo);

            var fechaResolucion = DateTime.UtcNow; // O usa reclamo.FechaResolucion si existe

            var reclamoResueltoEvent = new ReclamoResueltoEvent(
                reclamo.Id.Value,
                reclamo.UsuarioId.Value,
                resolucion.Value,
                fechaResolucion
            );

            await _mediator.Publish(reclamoResueltoEvent, cancellationToken);

            return Unit.Value;
        }
    }
}