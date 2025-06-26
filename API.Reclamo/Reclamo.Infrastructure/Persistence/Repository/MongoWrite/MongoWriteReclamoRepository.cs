using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Reclamo.Domain.Aggregates;
using Reclamo.Domain.Repositories;
using Reclamo.Infrastructure.Configurations;
using Reclamo.Domain.ValueObjects;

namespace Reclamo.Infrastructure.Persistence.Repository.MongoWrite
{
    /// <summary>
    /// Repositorio de escritura para reclamos en MongoDB.
    /// Permite operaciones CRUD sobre la colección de reclamos de escritura.
    /// </summary>
    public class MongoWriteReclamoRepository : IReclamoRepository
    {
        /// <summary>
        /// Colección de reclamos en la base de datos de escritura.
        /// </summary>
        private readonly IMongoCollection<BsonDocument> _reclamosCollection;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="MongoWriteReclamoRepository"/>.
        /// </summary>
        /// <param name="mongoConfig">Configuración de la base de datos de escritura.</param>
        public MongoWriteReclamoRepository(MongoWriteDbConfig mongoConfig)
        {
            _reclamosCollection = mongoConfig.Db.GetCollection<BsonDocument>("reclamo_write");
        }

        /// <summary>
        /// Agrega un nuevo reclamo a la base de datos de escritura.
        /// </summary>
        /// <param name="reclamo">Reclamo a agregar.</param>
        public async Task AddAsync(Aggregate_Reclamo reclamo)
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
            await _reclamosCollection.InsertOneAsync(doc);
        }

        /// <summary>
        /// Obtiene un reclamo por su identificador.
        /// </summary>
        /// <param name="id">Identificador del reclamo.</param>
        /// <returns>El reclamo encontrado o null si no existe.</returns>
        public async Task<Aggregate_Reclamo?> GetByIdAsync(ReclamoId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id.Value);
            var doc = await _reclamosCollection.Find(filter).FirstOrDefaultAsync();
            if (doc == null) return null;

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

        /// <summary>
        /// Obtiene todos los reclamos de la base de datos de escritura.
        /// </summary>
        /// <returns>Una colección de reclamos.</returns>
        public async Task<IEnumerable<Aggregate_Reclamo>> GetAllAsync()
        {
            var docs = await _reclamosCollection.Find(new BsonDocument()).ToListAsync();
            return docs.Select(doc => new Aggregate_Reclamo(
                new ReclamoId(doc["_id"].AsString),
                new UsuarioId(doc["usuarioId"].AsString),
                doc.Contains("subastaId") && !doc["subastaId"].IsBsonNull ? new SubastaId(doc["subastaId"].AsString) : null,
                new Motivo(doc["motivo"].AsString),
                new Descripcion(doc["descripcion"].AsString),
                doc.Contains("evidenciaUrl") && !doc["evidenciaUrl"].IsBsonNull ? new EvidenciaUrl(doc["evidenciaUrl"].AsString) : null,
                new EstadoReclamo(doc["estado"].AsString),
                doc.Contains("resolucion") && !doc["resolucion"].IsBsonNull ? new Resolucion(doc["resolucion"].AsString) : null
            ));
        }

        /// <summary>
        /// Actualiza un reclamo existente en la base de datos de escritura.
        /// </summary>
        /// <param name="reclamo">Reclamo actualizado.</param>
       public async Task UpdateAsync(Aggregate_Reclamo reclamo)
{
    var filter = Builders<BsonDocument>.Filter.Eq("_id", reclamo.Id.Value);
    var update = Builders<BsonDocument>.Update
        .Set("usuarioId", reclamo.UsuarioId.Value)
        .Set("subastaId", reclamo.SubastaId )
        .Set("motivo", reclamo.Motivo.Value)
        .Set("descripcion", reclamo.Descripcion.Value)
        .Set("evidenciaUrl", reclamo.EvidenciaUrl )
        .Set("estado", reclamo.Estado.Value)
        .Set("resolucion", reclamo.Resolucion )
        .Set("fechaResolucion", reclamo.FechaResolucion );

    await _reclamosCollection.UpdateOneAsync(filter, update);
}

        /// <summary>
        /// Elimina un reclamo de la base de datos de escritura.
        /// </summary>
        /// <param name="reclamo">Reclamo a eliminar.</param>
        public async Task DeleteAsync(Aggregate_Reclamo reclamo)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", reclamo.Id.Value);
            await _reclamosCollection.DeleteOneAsync(filter);
        }
        /// <summary>
        /// Marca un reclamo como resuelto, actualizando su estado, resolución y fecha de resolución.
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
}