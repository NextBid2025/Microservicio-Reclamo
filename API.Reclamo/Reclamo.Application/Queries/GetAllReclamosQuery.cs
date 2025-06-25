using MediatR;
using System.Collections.Generic;
using Reclamo.Domain.Aggregates;

namespace Reclamo.Application.Queries
{
    /// <summary>
    /// Consulta para obtener todos los reclamos.
    /// Implementa <see cref="IRequest{TResponse}"/> para ser manejada por MediatR.
    /// </summary>
    public class GetAllReclamosQuery : IRequest<IEnumerable<Aggregate_Reclamo>>
    {
    }
}