namespace TransitX.Common.Service
{
    /// <summary>
    /// Interface for a generic repository providing CRUD operations for entities of type TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public interface IService<TEntity>
    {
        /// <summary>
        /// Retrieves all entities from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all entities in the repository.</returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Retrieves a page of entities from the repository.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size (number of entities per page).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities representing the requested page.</returns>
        Task<IEnumerable<TEntity>> GetPage(int pageNumber, int pageSize);

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity with the specified identifier.</returns>
        Task<TEntity> GetById(string id);

        /// <summary>
        /// Inserts a new entity into the repository.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Insert(TEntity entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to update.</param>
        /// <param name="entity">The updated entity data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the update operation was successful.</returns>
        Task<bool> Update(string id, TEntity entity);

        /// <summary>
        /// Deletes an entity from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the delete operation was successful.</returns>
        Task<bool> Delete(string id);

        /// <summary>
        /// Deletes all entities from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the delete operation was successful.</returns>
        Task<bool> DeleteAll();
    }
}
