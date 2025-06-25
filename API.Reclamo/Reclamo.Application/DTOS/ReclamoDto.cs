namespace Reclamo.Application;

public class ReclamoDto
{
    public Guid Id { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string? EvidenciaUrl { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string? Resolucion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaResolucion { get; set; }
}