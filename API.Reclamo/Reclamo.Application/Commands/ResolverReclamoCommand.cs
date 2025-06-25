using MediatR;

namespace Reclamo.Application.Commands;

public class ResolverReclamoCommand : IRequest<Unit>
{
    public string ReclamoId { get; set; }
    public string Resolucion { get; set; }
    public string EstadoFinal { get; set; }
    public string UsuarioId { get; set; }
}