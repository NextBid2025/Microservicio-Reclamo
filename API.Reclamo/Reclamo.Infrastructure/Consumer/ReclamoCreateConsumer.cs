using System;
using System.Threading.Tasks;
using MassTransit;
using Reclamo.Domain.Events;
using Reclamo.Domain.Repositories;
using Reclamo.Domain.Aggregates;
using Reclamo.Domain.Factories;
using Reclamo.Domain.ValueObjects;

namespace Reclamo.Infrastructure.Consumer;

public class ReclamoCreateConsumer : IConsumer<ReclamoCreateEvent>
{
    private readonly IReclamoReadRepository _reclamoReadRepository;

    public ReclamoCreateConsumer(IReclamoReadRepository reclamoReadRepository)
    {
        _reclamoReadRepository = reclamoReadRepository;
    }

    public async Task Consume(ConsumeContext<ReclamoCreateEvent> context)
    {
        var message = context.Message;
        Console.WriteLine($"Mensaje recibido: {message}");

        var reclamo = ReclamoFactory.Create(
            new ReclamoId(message.ReclamoId),
            new UsuarioId(message.UsuarioId),
            string.IsNullOrEmpty(message.SubastaId) ? null : new SubastaId(message.SubastaId),
            new Motivo(message.Motivo),
            new Descripcion(message.Descripcion),
            string.IsNullOrEmpty(message.EvidenciaUrl) ? null : new EvidenciaUrl(message.EvidenciaUrl),
            new EstadoReclamo(message.Estado),
            null // Resolución, si aplica
        );

        await _reclamoReadRepository.AddAsync(reclamo);
        Console.WriteLine("Reclamo insertado en la base de datos de lectura.");
    }
}