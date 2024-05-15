using Microsoft.AspNetCore.Mvc;
using TransitX.API.Controllers.Interfaces;
using TransitX.Common.Service;

namespace TransitX.API.Controllers
{
    /// <summary>
    /// Base controller for handling CRUD operations for any entity type TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public abstract class BaseController<TEntity> : ControllerBase, IBaseController<TEntity> where TEntity : class
    {
        protected readonly IService<TEntity> _service;

        public BaseController(IService<TEntity> service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets a page of entities.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A page of entities.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var items = await _service.GetPage(pageNumber, pageSize);
                return Ok(items);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest("Error retrieving entities: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>The entity with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> GetById(string id)
        {
            try
            {
                var entity = await _service.GetById(id);
                return entity != null ? Ok(entity) : NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest("Error retrieving entity by ID: " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity.</returns>
        [HttpPost]
        public async Task<ActionResult<TEntity>> Create(TEntity entity)
        {
            try
            {
                await _service.Insert(entity);
                return CreatedAtAction(nameof(GetById), new { id = typeof(TEntity).GetProperty("Id").GetValue(entity).ToString() }, entity);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest("Error creating entity: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">The ID of the entity to update.</param>
        /// <param name="entity">The updated entity.</param>
        /// <returns>No content if successful, or not found if the entity does not exist.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, TEntity entity)
        {
            try
            {
                var result = await _service.Update(id, entity);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest("Error updating entity: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>No content if successful, or not found if the entity does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _service.Delete(id);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest("Error deleting entity: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes all entities.
        /// </summary>
        /// <returns>No content if successful.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                var result = await _service.DeleteAll();
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest("Error deleting all entities: " + ex.Message);
            }
        }
    }
}
