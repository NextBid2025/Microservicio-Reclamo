using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Reclamo.Application.Queries;
using Reclamo.Domain.Aggregates;
using Reclamo.Domain.Repositories;

namespace Reclamo.Application.Handlers
{
    public class GetAllReclamosQueryHandler : IRequestHandler<GetAllReclamosQuery, IEnumerable<Aggregate_Reclamo>>
    {
        private readonly IReclamoRepository _reclamoRepository;

        public GetAllReclamosQueryHandler(IReclamoRepository reclamoRepository)
        {
            _reclamoRepository = reclamoRepository;
        }

        public async Task<IEnumerable<Aggregate_Reclamo>> Handle(GetAllReclamosQuery request, CancellationToken cancellationToken)
        {
            return await _reclamoRepository.GetAllAsync();
        }
    }
}