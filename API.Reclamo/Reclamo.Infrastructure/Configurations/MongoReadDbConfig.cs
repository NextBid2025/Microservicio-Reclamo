using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Reclamo.Infrastructure.Configurations;

/// <summary>
/// Configuración para la conexión de solo lectura a la base de datos MongoDB.
/// </summary>
public class MongoReadDbConfig
{
    private readonly MongoClient _client;

    /// <summary>
    /// Obtiene la instancia de la base de datos MongoDB configurada para lectura.
    /// </summary>
    public IMongoDatabase Db { get; }

    /// <summary>
    /// Obtiene el cliente de MongoDB utilizado para la conexión de lectura.
    /// </summary>
    public MongoClient Client => _client;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="MongoReadDbConfig"/>.
    /// Lee las variables de entorno para la cadena de conexión y el nombre de la base de datos.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Se lanza si la configuración de la cadena de conexión o el nombre de la base de datos no están definidos.
    /// </exception>
    public MongoReadDbConfig()
    {
        string connectionUri = Environment.GetEnvironmentVariable("MONGODB_CNN_READ");
        string databaseName = Environment.GetEnvironmentVariable("MONGODB_NAME_READ");

        if (string.IsNullOrWhiteSpace(connectionUri) || string.IsNullOrWhiteSpace(databaseName))
            throw new ArgumentException("La configuración de MongoDB para lectura no está definida.");

        var settings = MongoClientSettings.FromConnectionString(connectionUri);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _client = new MongoClient(settings);
        Db = _client.GetDatabase(databaseName);
    }
}