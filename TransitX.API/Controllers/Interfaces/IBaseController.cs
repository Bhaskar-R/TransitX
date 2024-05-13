using Microsoft.AspNetCore.Mvc;

namespace TransitX.API.Controllers.Interfaces
{
    /// <summary>
    /// Interface for base controller handling CRUD operations for any entity type TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public interface IBaseController<TEntity> where TEntity : class
    {
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
        Task<ActionResult<IEnumerable<TEntity>>> Get(int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>An action result containing the retrieved entity or a not found result if no entity is found with the specified ID.</returns>
        Task<ActionResult<TEntity>> GetById(string id);

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>An action result containing the created entity.</returns>
        Task<ActionResult<TEntity>> Create(TEntity entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">The ID of the entity to update.</param>
        /// <param name="entity">The updated entity data.</param>
        /// <returns>An action result indicating success or failure of the update operation.</returns>
        Task<IActionResult> Update(string id, TEntity entity);

        /// <summary>
        /// Deletes an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>An action result indicating success or failure of the delete operation.</returns>
        Task<IActionResult> Delete(string id);

        /// <summary>
        /// Deletes all entities.
        /// </summary>
        /// <returns>An action result indicating success or failure of the delete operation.</returns>
        Task<IActionResult> DeleteAll();
    }
}
