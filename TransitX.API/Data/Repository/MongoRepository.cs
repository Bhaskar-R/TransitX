using MongoDB.Driver;
using TransitX.API.Data.Interfaces;
using TransitX.Common.Repository;

namespace TransitX.API.Data.Repository
{
    /// <summary>
    /// Implementation of IRepository<TEntity> for MongoDB.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="mongoDbService">The MongoDB service.</param>
        /// <param name="collectionName">The name of the collection.</param>
        public MongoRepository(IMongoDbService mongoDbService, string collectionName = null)
        {
            if (mongoDbService == null)
            {
                throw new ArgumentNullException(nameof(mongoDbService));
            }

            if (collectionName == null)
            {
                collectionName = typeof(TEntity).Name.ToLower();
            }
            _collection = mongoDbService.GetCollection<TEntity>(collectionName);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                return await _collection.Find(FilterDefinition<TEntity>.Empty).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving all entities.", ex);
            }
        }

        /// <inheritdoc />
        public async Task<TEntity> GetById(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            try
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", id);
                return await _collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while retrieving entity by ID '{id}'.", ex);
            }
        }

        /// <inheritdoc />
        public async Task Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await _collection.InsertOneAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while inserting entity.", ex);
            }
        }

        /// <inheritdoc />
        public async Task<bool> Update(string id, TEntity entity)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", id);
                var result = await _collection.ReplaceOneAsync(filter, entity);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while updating entity with ID '{id}'.", ex);
            }
        }

        /// <inheritdoc />
        public async Task<bool> Delete(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            try
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", id);
                var result = await _collection.DeleteOneAsync(filter);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while deleting entity with ID '{id}'.", ex);
            }
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAll()
        {
            try
            {
                var deleteResult = await _collection.DeleteManyAsync(FilterDefinition<TEntity>.Empty);
                return deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting all entities.", ex);
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetPage(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than zero.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");
            }

            try
            {
                // Calculate the number of items to skip
                int skip = (pageNumber - 1) * pageSize;

                // Retrieve the page of items from the collection
                var items = await _collection.Find(_ => true)
                                             .Skip(skip)
                                             .Limit(pageSize)
                                             .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving paginated entities.", ex);
            }
        }
    }
}
