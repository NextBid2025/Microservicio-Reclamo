using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Reclamo.Infrastructure.Persistence.Repository.MongoRead.Documents;

public class ReclamoMongoRead
{
    [BsonId]
    [BsonElement("_id")]
    public required string Id { get; set; }

    [BsonElement("usuarioId")]
    public required string UsuarioId { get; set; }

    [BsonElement("subastaId")]
    public string? SubastaId { get; set; }

    [BsonElement("motivo")]
    public required string Motivo { get; set; }

    [BsonElement("descripcion")]
    public required string Descripcion { get; set; }

    [BsonElement("evidenciaUrl")]
    public string? EvidenciaUrl { get; set; }

    [BsonElement("estado")]
    public required string Estado { get; set; }

    [BsonElement("fechaCreacion")]
    public required DateTime FechaCreacion { get; set; }
}