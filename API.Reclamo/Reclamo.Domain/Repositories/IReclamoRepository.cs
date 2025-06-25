using Reclamo.Domain.ValueObjects;

namespace Reclamo.Domain.Repositories;

using Reclamo.Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IReclamoRepository
{
    Task<Aggregate_Reclamo?> GetByIdAsync(ReclamoId id);
    Task<IEnumerable<Aggregate_Reclamo>> GetAllAsync();
    Task AddAsync(Aggregate_Reclamo reclamo);
    Task UpdateAsync(Aggregate_Reclamo reclamo);
    Task DeleteAsync(Aggregate_Reclamo reclamo);
}