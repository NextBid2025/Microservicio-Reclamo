using MediatR;
using System.Collections.Generic;
using Reclamo.Domain.Aggregates;

namespace Reclamo.Application.Queries
{
    /// <summary>
    /// Consulta para obtener reclamos pendientes.
    /// </summary>
    public class GetPendingReclamosQuery : IRequest<IEnumerable<Aggregate_Reclamo>>
    {
    }
}