using MediatR;
using System;

namespace Reclamo.Domain.Events;

/// <summary>
/// Evento de dominio que representa la creación de un reclamo.
/// Contiene información relevante del reclamo recién creado.
/// </summary>
public class ReclamoCreadoEvent : INotification
{
    public string ReclamoId { get; }
    public string UsuarioId { get; }
    public string? SubastaId { get; }
    public string Motivo { get; }
    public string Descripcion { get; }
    public string? EvidenciaUrl { get; }
    public string Estado { get; }
    public string? Resolucion { get; }
    public DateTime FechaCreacion { get; }

    /// <summary>
    /// Inicializa una nueva instancia del evento <see cref="ReclamoCreadoEvent"/>.
    /// </summary>
    public ReclamoCreadoEvent(
        string reclamoId,
        string usuarioId,
        string? subastaId,
        string motivo,
        string descripcion,
        string? evidenciaUrl,
        string estado,
        string? resolucion,
        DateTime fechaCreacion)
    {
        ReclamoId = reclamoId;
        UsuarioId = usuarioId;
        SubastaId = subastaId;
        Motivo = motivo;
        Descripcion = descripcion;
        EvidenciaUrl = evidenciaUrl;
        Estado = estado;
        Resolucion = resolucion;
        FechaCreacion = fechaCreacion;
    }
}