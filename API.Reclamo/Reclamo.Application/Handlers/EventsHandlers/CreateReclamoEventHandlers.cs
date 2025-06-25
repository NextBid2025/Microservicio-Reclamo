using MediatR;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Reclamo.Domain.Events;

namespace Reclamo.Application.Handlers.EventsHandlers
{
    /// <summary>
    /// Manejador de eventos para la creación de reclamos.
    /// Envía el evento <see cref="ReclamoCreadoEvent"/> a la cola de RabbitMQ.
    /// </summary>
    public class CreateReclamoEventHandler : INotificationHandler<ReclamoCreadoEvent>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de eventos de creación de reclamos.
        /// </summary>
        /// <param name="sendEndpointProvider">Proveedor de endpoints para enviar mensajes.</param>
        public CreateReclamoEventHandler(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider ?? throw new ArgumentNullException(nameof(sendEndpointProvider));
        }

        /// <summary>
        /// Maneja el evento de creación de reclamo y lo envía a la cola de RabbitMQ.
        /// </summary>
        public async Task Handle(ReclamoCreadoEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Reclamo creado: {notification.Motivo}");
            await _sendEndpointProvider.Send(notification, cancellationToken);
        }
    }
}