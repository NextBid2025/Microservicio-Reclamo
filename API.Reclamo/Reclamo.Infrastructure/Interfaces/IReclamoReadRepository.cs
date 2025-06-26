using Reclamo.Domain.ValueObjects;

namespace Reclamo.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Reclamo.Domain.Aggregates;

public interface IReclamoReadRepository
{
    Task AddAsync(Aggregate_Reclamo reclamo);
    Task<Aggregate_Reclamo> GetByIdAsync(string reclamoId);
    Task<IEnumerable<Aggregate_Reclamo>> GetAllAsync();
    Task UpdateAsync(Aggregate_Reclamo reclamo);
    Task DeleteAsync(string reclamoId);
    Task ResolverAsync(ReclamoId id, string resolucion);
    
    
}