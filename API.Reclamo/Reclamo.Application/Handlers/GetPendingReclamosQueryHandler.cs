using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Reclamo.Application.Queries;
using Reclamo.Domain.Aggregates;
using Reclamo.Domain.Repositories;

namespace Reclamo.Application.Handlers
{
    public class GetPendingReclamosQueryHandler : IRequestHandler<GetPendingReclamosQuery, IEnumerable<Aggregate_Reclamo>>
    {
        private readonly IReclamoRepository _reclamoRepository;

        public GetPendingReclamosQueryHandler(IReclamoRepository reclamoRepository)
        {
            _reclamoRepository = reclamoRepository;
        }

        public async Task<IEnumerable<Aggregate_Reclamo>> Handle(GetPendingReclamosQuery request, CancellationToken cancellationToken)
        {
            var allReclamos = await _reclamoRepository.GetAllAsync();
          
            return allReclamos.Where(r => r.Estado.Value == "Pendiente");
        }
    }
}