using MongoDB.Driver;

namespace TransitX.API.Data.Interfaces
{
    /// <summary>
    /// Interface for interacting with MongoDB database collections.
    /// </summary>
    public interface IMongoDbService
    {
        /// <summary>
        /// Gets a collection from the MongoDB database.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collectionName">The name of the collection.</param>
        /// <returns>The MongoDB collection.</returns>
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
