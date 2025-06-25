using MediatR;
using Reclamo.Domain.ValueObjects;

namespace Reclamo.Application.Commands;

/// <summary>
/// Comando para crear un nuevo reclamo.
/// Implementa <see cref="IRequest{TResponse}"/> para ser manejado por MediatR.
/// </summary>
public class CreateReclamoCommand : IRequest<string>
{
    /// <summary>
    /// Identificador del usuario que presenta el reclamo.
    /// </summary>
    public UsuarioId UsuarioId { get; }

    /// <summary>
    /// Identificador de la subasta asociada (opcional).
    /// </summary>
    public SubastaId? SubastaId { get; }

    /// <summary>
    /// Motivo del reclamo.
    /// </summary>
    public string Motivo { get; }

    /// <summary>
    /// Descripción del reclamo.
    /// </summary>
    public string Descripcion { get; }

    /// <summary>
    /// URL de la evidencia adjunta (opcional).
    /// </summary>
    public string? EvidenciaUrl { get; }

    /// <summary>
    /// Constructor para inicializar el comando de creación de reclamo.
    /// </summary>
    public CreateReclamoCommand(
        UsuarioId usuarioId,
        SubastaId? subastaId,
        string motivo,
        string descripcion,
        string? evidenciaUrl)
    {
        UsuarioId = usuarioId;
        SubastaId = subastaId;
        Motivo = motivo;
        Descripcion = descripcion;
        EvidenciaUrl = evidenciaUrl;
    }
}