using MongoDB.Driver;
using TransitX.API.Data.Interfaces;

namespace TransitX.API.Data
{
    /// <summary>
    /// Service for interacting with MongoDB database.
    /// </summary>
    public class MongoDbService : IMongoDbService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Gets the MongoDB database.
        /// </summary>
        public IMongoDatabase Database { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbService"/> class with the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="ArgumentException">Thrown when the database connection string is missing or empty.</exception>
        /// <exception cref="Exception">Thrown when an error occurs while connecting to the MongoDB database.</exception>
        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            var connectionString = _configuration.GetConnectionString("DbConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Database connection string is missing or empty.", nameof(connectionString));
            }

            try
            {
                var mongoUrl = new MongoUrl(connectionString);
                var mongoClient = new MongoClient(mongoUrl);
                Database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Error connecting to the MongoDB database.", ex);
            }
        }

        /// <summary>
        /// Gets the MongoDB collection for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collectionName">The name of the collection.</param>
        /// <returns>The MongoDB collection.</returns>
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException("Collection name is missing or empty.", nameof(collectionName));
            }

            return Database.GetCollection<T>(collectionName);
        }
    }
}
