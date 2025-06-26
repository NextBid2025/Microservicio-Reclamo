using System;
using MongoDB.Driver;

namespace Reclamo.Infrastructure.Configurations;

/// <summary>
/// Configuración para la conexión de escritura a la base de datos MongoDB.
/// </summary>
public class MongoWriteDbConfig
{
    private readonly MongoClient _client;

    /// <summary>
    /// Obtiene la instancia de la base de datos MongoDB configurada para escritura.
    /// </summary>
    public IMongoDatabase Db { get; }

    /// <summary>
    /// Obtiene el cliente de MongoDB utilizado para la conexión de escritura.
    /// </summary>
    public MongoClient Client => _client;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="MongoWriteDbConfig"/>.
    /// Lee las variables de entorno para la cadena de conexión y el nombre de la base de datos.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Se lanza si la configuración de la cadena de conexión o el nombre de la base de datos no están definidos.
    /// </exception>
    public MongoWriteDbConfig()
    {
        string connectionUri = Environment.GetEnvironmentVariable("MONGODB_CNN_WRITE");
        string databaseName = Environment.GetEnvironmentVariable("MONGODB_NAME_WRITE");

        if (string.IsNullOrWhiteSpace(connectionUri) || string.IsNullOrWhiteSpace(databaseName))
            throw new ArgumentException("La configuración de MongoDB para escritura no está definida.");

        var settings = MongoClientSettings.FromConnectionString(connectionUri);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        settings.ConnectTimeout = TimeSpan.FromSeconds(60);

        _client = new MongoClient(settings);
        Db = _client.GetDatabase(databaseName);
    }
}