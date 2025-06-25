using MediatR;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Reclamo.Domain.Events;

namespace Reclamo.Application.Handlers.EventsHandlers
{
    /// <summary>
    /// Manejador de eventos para la resolución de reclamos.
    /// Se encarga de manejar el evento <see cref="ReclamoResueltoEvent"/> y enviarlo a la cola de RabbitMQ.
    /// </summary>
    public class ReclamoResueltoEventHandler : INotificationHandler<ReclamoResueltoEvent>
    {
        private readonly ISendEndpointProvider _publishEndpoint;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de eventos de resolución de reclamos.
        /// </summary>
        /// <param name="publishEndpoint">Proveedor de endpoints para enviar mensajes.</param>
        public ReclamoResueltoEventHandler(ISendEndpointProvider publishEndpoint)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        /// <summary>
        /// Maneja el evento de reclamo resuelto y lo envía a la cola de RabbitMQ.
        /// </summary>
        /// <param name="notification">Evento de reclamo resuelto.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        public async Task Handle(ReclamoResueltoEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Reclamo resuelto: {notification.ReclamoId}");
            await _publishEndpoint.Send(notification);
        }
    }
}