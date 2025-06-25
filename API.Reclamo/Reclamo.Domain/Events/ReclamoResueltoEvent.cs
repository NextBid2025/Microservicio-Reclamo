// ReclamoResueltoEvent.cs
using MediatR;
using System;

namespace Reclamo.Domain.Events;

public class ReclamoResueltoEvent : INotification
{
    public string ReclamoId { get; }
    public string Resolucion { get; }
    public string Estado { get; }
    public DateTime FechaResolucion { get; }

    public ReclamoResueltoEvent(
        string reclamoId,
        string resolucion,
        string estado,
        DateTime fechaResolucion)
    {
        ReclamoId = reclamoId;
        Resolucion = resolucion;
        Estado = estado;
        FechaResolucion = fechaResolucion;
    }
}