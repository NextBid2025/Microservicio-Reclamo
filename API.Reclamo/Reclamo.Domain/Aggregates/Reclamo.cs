using Reclamo.Domain.ValueObjects;

namespace Reclamo.Domain.Aggregates;


using System;


/// <summary>
/// Representa un reclamo dentro del dominio.
/// </summary>
public class Aggregate_Reclamo
{
    /// <summary>
    /// Identificador único del reclamo.
    /// </summary>
    public ReclamoId Id { get; private set; }

    /// <summary>
    /// Identificador del usuario que presenta el reclamo.
    /// </summary>
    public UsuarioId UsuarioId { get; private set; }

    /// <summary>
    /// Identificador de la subasta asociada (opcional).
    /// </summary>
    public SubastaId? SubastaId { get; private set; }

    /// <summary>
    /// Motivo del reclamo.
    /// </summary>
    public Motivo Motivo { get; private set; }

    /// <summary>
    /// Descripción del reclamo.
    /// </summary>
    public Descripcion Descripcion { get; private set; }

    /// <summary>
    /// URL de la evidencia adjunta (opcional).
    /// </summary>
    public EvidenciaUrl? EvidenciaUrl { get; private set; }

    /// <summary>
    /// Estado actual del reclamo.
    /// </summary>
    public EstadoReclamo Estado { get; private set; }

    /// <summary>
    /// Resolución del reclamo (opcional).
    /// </summary>
    public Resolucion? Resolucion { get; private set; }

    /// <summary>
    /// Fecha de creación del reclamo.
    /// </summary>
    public DateTime FechaCreacion { get; private set; }

    /// <summary>
    /// Fecha de resolución del reclamo (opcional).
    /// </summary>
    public DateTime? FechaResolucion { get; private set; }

    /// <summary>
    /// Constructor privado para EF.
    /// </summary>
    private Reclamo() { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Reclamo"/>.
    /// </summary>
    public Reclamo(
        ReclamoId id,
        UsuarioId usuarioId,
        SubastaId? subastaId,
        Motivo motivo,
        Descripcion descripcion,
        EvidenciaUrl? evidenciaUrl,
        EstadoReclamo estado,
        Resolucion? resolucion
    )
    {
        Id = id;
        UsuarioId = usuarioId;
        SubastaId = subastaId;
        Motivo = motivo;
        Descripcion = descripcion;
        EvidenciaUrl = evidenciaUrl;
        Estado = estado;
        Resolucion = resolucion;
        FechaCreacion = DateTime.UtcNow;
        FechaResolucion = null;
        Resolucion = null;
    }

    /// <summary>
    /// Actualiza el estado y la resolución del reclamo.
    /// </summary>
    public void Resolver(EstadoReclamo nuevoEstado, Resolucion resolucion)
    {
        Estado = nuevoEstado;
        Resolucion = resolucion;
        FechaResolucion = DateTime.UtcNow;
    }
}