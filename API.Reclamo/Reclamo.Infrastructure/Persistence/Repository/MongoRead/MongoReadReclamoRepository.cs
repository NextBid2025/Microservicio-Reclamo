using MongoDB.Bson;
using MongoDB.Driver;
using Reclamo.Domain.Repositories;
using Reclamo.Domain.Aggregates;
using Reclamo.Infrastructure.Configurations;
using Reclamo.Domain.ValueObjects;

namespace Reclamo.Infrastructure.Persistence.Repository.MongoRead;

public class MongoReadReclamoRepository : IReclamoReadRepository
{
    private readonly IMongoCollection<BsonDocument> _reclamosCollection;

    public MongoReadReclamoRepository(MongoReadDbConfig mongoConfig)
    {
        _reclamosCollection = mongoConfig.Db.GetCollection<BsonDocument>("reclamo_read");
    }

    public async Task AddAsync(Aggregate_Reclamo reclamo)
    {
        var doc = ToBsonDocument(reclamo);
        await _reclamosCollection.InsertOneAsync(doc);
    }

    public async Task<Aggregate_Reclamo?> GetByIdAsync(string id)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
        var bsonReclamo = await _reclamosCollection.Find(filter).FirstOrDefaultAsync();
        return bsonReclamo == null ? null : ToAggregateReclamo(bsonReclamo);
    }

    public async Task<IEnumerable<Aggregate_Reclamo>> GetAllAsync()
    {
        var reclamos = await _reclamosCollection.Find(new BsonDocument()).ToListAsync();
        return reclamos.Select(ToAggregateReclamo);
    }

    public async Task UpdateAsync(Aggregate_Reclamo reclamo)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", reclamo.Id.Value);
        var doc = ToBsonDocument(reclamo);
        await _reclamosCollection.ReplaceOneAsync(filter, doc);
    }

    public async Task DeleteAsync(string reclamoId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", reclamoId);
        var result = await _reclamosCollection.DeleteOneAsync(filter);
        if (result.DeletedCount == 0)
            throw new KeyNotFoundException("No se encontró el reclamo para eliminar.");
    }

    // Métodos de mapeo
    private static Aggregate_Reclamo ToAggregateReclamo(BsonDocument doc)
    {
        return new Aggregate_Reclamo(
            new ReclamoId(doc["_id"].AsString),
            new UsuarioId(doc["usuarioId"].AsString),
            doc.Contains("subastaId") && !doc["subastaId"].IsBsonNull ? new SubastaId(doc["subastaId"].AsString) : null,
            new Motivo(doc["motivo"].AsString),
            new Descripcion(doc["descripcion"].AsString),
            doc.Contains("evidenciaUrl") && !doc["evidenciaUrl"].IsBsonNull ? new EvidenciaUrl(doc["evidenciaUrl"].AsString) : null,
            new EstadoReclamo(doc["estado"].AsString),
            doc.Contains("resolucion") && !doc["resolucion"].IsBsonNull ? new Resolucion(doc["resolucion"].AsString) : null
        );
    }

    private static BsonDocument ToBsonDocument(Aggregate_Reclamo reclamo)
    {
        var doc = new BsonDocument
        {
            { "_id", reclamo.Id.Value },
            { "usuarioId", reclamo.UsuarioId.Value },
            { "subastaId", reclamo.SubastaId != null ? reclamo.SubastaId.Value : BsonNull.Value },
            { "motivo", reclamo.Motivo.Value },
            { "descripcion", reclamo.Descripcion.Value },
            { "evidenciaUrl", reclamo.EvidenciaUrl != null ? reclamo.EvidenciaUrl.Value : BsonNull.Value },
            { "estado", reclamo.Estado.Value },
            { "resolucion", reclamo.Resolucion != null ? reclamo.Resolucion.Value : BsonNull.Value },
            { "fechaCreacion", reclamo.FechaCreacion },
            { "fechaResolucion", reclamo.FechaResolucion != null ? (BsonValue)reclamo.FechaResolucion : BsonNull.Value }
        };
        return doc;
    }
    
    /// <summary>
    /// Marca un reclamo como resuelto en la base de datos de lectura, actualizando su estado, resolución y fecha de resolución.
    /// </summary>
    /// <param name="id">Identificador del reclamo.</param>
    /// <param name="resolucion">Texto de la resolución.</param>
    /// <returns>Una tarea que representa la operación asincrónica.</returns>
    public async Task ResolverAsync(ReclamoId id, string resolucion)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", id.Value);
        var update = Builders<BsonDocument>.Update
            .Set("estado", "Resuelto")
            .Set("resolucion", resolucion)
            .Set("fechaResolucion", DateTime.UtcNow);

        await _reclamosCollection.UpdateOneAsync(filter, update);
    }
}