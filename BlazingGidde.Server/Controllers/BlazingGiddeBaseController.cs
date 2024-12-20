using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BlazingGidde.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlazingGiddeBaseController<TEntity, TDataContext> : ControllerBase, IBlazingGiddeBaseController<TEntity>
        where TEntity : class
        where TDataContext : DbContext
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly ILogger<BlazingGiddeBaseController<TEntity, TDataContext>> _logger;

        public BlazingGiddeBaseController(IRepository<TEntity> repository, ILogger<BlazingGiddeBaseController<TEntity, TDataContext>> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all entities.");
                var result = (await _repository.GetAll()).ToList();

                if (result.Any())
                {
                    _logger.LogInformation("Entities fetched successfully.");
                    return Ok(new APIListOfEntityResponse<TEntity>
                    {
                        Success = true,
                        Items = result
                    });
                }

                _logger.LogWarning("No entities found.");
                return Ok(new APIListOfEntityResponse<TEntity>
                {
                    Success = true,
                    ErrorMessages = new List<string> { "No entities found." }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all entities.");
                return StatusCode(StatusCodes.Status500InternalServerError, new APIListOfEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "An internal server error occurred while fetching entities." }
                });
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<APIEntityResponse<TEntity>>> GetById([FromRoute] string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                _logger.LogWarning("GetById called with null or empty ID.");
                return BadRequest(new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "ID cannot be null or empty." }
                });
            }

            try
            {
                _logger.LogInformation("Fetching entity with ID: {Id}", Id);
                TEntity? entity = null;
                //si TEntity respeta la interfaz de model base
                if (typeof(IModelBase).IsAssignableFrom(typeof(TEntity)))
                {
                    if (int.TryParse(Id, out var integerId))
                    {
                        entity = await _repository.GetByID(integerId);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid ID format for IModelBase entity.");
                    }
                }
                else
                {
                    entity = await _repository.GetByID(Id);
                }

                if (entity is not null)
                {
                    _logger.LogInformation("Entity with ID: {Id} fetched successfully.", Id);
                    return Ok(new APIEntityResponse<TEntity>
                    {
                        Success = true,
                        Items = entity
                    });
                }

                _logger.LogWarning("Entity with ID: {Id} not found.", Id);
                return NotFound(new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { $"Entity with ID {Id} was not found." }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the entity with ID: {Id}.", Id);
                return StatusCode(StatusCodes.Status500InternalServerError, new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "An internal server error occurred while fetching the entity." }
                });
            }
        }

        [HttpPost("getwithfilter")]
        public async Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetWithFilter([FromBody] QueryFilter<TEntity> Filter)
        {
            if (Filter == null)
            {
                _logger.LogWarning("GetWithFilter called with null filter.");
                return BadRequest(new APIListOfEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "Filter cannot be null." }
                });
            }

            try
            {
                _logger.LogInformation("Fetching entities with filter.");
                var result = await _repository.Get(Filter);

                if (result.Any())
                {
                    _logger.LogInformation("Entities fetched successfully with filter.");
                    return Ok(new APIListOfEntityResponse<TEntity>
                    {
                        Success = true,
                        Items = result.ToList()
                    });
                }

                _logger.LogWarning("No entities found with the provided filter.");
                return Ok(new APIListOfEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "No entities match the provided filter criteria." }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching entities with filter.");
                return StatusCode(StatusCodes.Status500InternalServerError, new APIListOfEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "An internal server error occurred while fetching entities with the provided filter." }
                });
            }
        }

        [HttpPost("getwithLinqfilter")]
        public async Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetWithLinqFilter([FromBody] LinqQueryFilter<TEntity> linqQueryFilter)
        {
            if (linqQueryFilter == null)
            {
                _logger.LogWarning("GetWithLinqFilter called with null LINQ filter.");
                return BadRequest(new APIListOfEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "LINQ filter cannot be null." }
                });
            }

            try
            {
                _logger.LogInformation("Fetching entities with LINQ filter.");
                var result = await _repository.Get(linqQueryFilter);

                if (result.Any())
                {
                    _logger.LogInformation("Entities fetched successfully with LINQ filter.");
                    return Ok(new APIListOfEntityResponse<TEntity>
                    {
                        Success = true,
                        Items = result.ToList()
                    });
                }

                _logger.LogWarning("No entities found with the provided LINQ filter.");
                return Ok(new APIListOfEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "No entities match the provided LINQ filter criteria." }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching entities with LINQ filter.");
                return StatusCode(StatusCodes.Status500InternalServerError, new APIListOfEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "An internal server error occurred while fetching entities with the provided LINQ filter." }
                });
            }
        }

        [HttpPost("GetTotalCount")]
        public async Task<ActionResult<int>> GetTotalCount([FromBody] LinqQueryFilter<TEntity> queryFilter)
        {
            if (queryFilter == null)
            {
                _logger.LogWarning("GetTotalCount called with null query filter.");
                return BadRequest(new { Success = false, ErrorMessages = new List<string> { "Query filter cannot be null." } });
            }

            try
            {
                _logger.LogInformation("Fetching total count of entities with provided filter.");
                var result = await _repository.GetTotalCount(queryFilter);
                return Ok(new { Success = true, Count = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching total count.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, ErrorMessages = new List<string> { "An internal server error occurred while fetching the total count." } });
            }
        }

        [HttpPost]
        public async Task<ActionResult<APIEntityResponse<TEntity>>> Post([FromBody] TEntity Entity)
        {
            if (Entity == null)
            {
                _logger.LogWarning("Post called with null entity.");
                return BadRequest(new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "Entity cannot be null." }
                });
            }

            try
            {
                _logger.LogInformation("Inserting a new entity.");

                if (Entity is ISupportTimeStamp timeStampedEntity)
                {
                    timeStampedEntity.CreateDate = DateTime.UtcNow;
                    timeStampedEntity.CreateUser = User.Identity?.Name ?? "anonymous";
                }

                var insertedEntity = await _repository.Insert(Entity);

                _logger.LogInformation("Entity inserted successfully.");
                return CreatedAtAction(nameof(GetById), new { Id = GetEntityId(insertedEntity!) }, new APIEntityResponse<TEntity>
                {
                    Success = true,
                    Items = insertedEntity
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting a new entity.");
                return StatusCode(StatusCodes.Status500InternalServerError, new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "An internal server error occurred while inserting the entity." }
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult<APIEntityResponse<TEntity>>> Put([FromBody] TEntity Entity)
        {
            if (Entity == null)
            {
                _logger.LogWarning("Put called with null entity.");
                return BadRequest(new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "Entity cannot be null." }
                });
            }

            try
            {
                _logger.LogInformation("Updating an entity.");
                if (Entity is ISupportTimeStamp timeStampedEntity)
                {
                    timeStampedEntity.UpdateDate = DateTime.UtcNow;
                    timeStampedEntity.UpdateUser = User.Identity?.Name ?? "anonymous";
                }

                var updateEntity = await _repository.Update(Entity);

                if (updateEntity != null)
                {
                    _logger.LogInformation("Entity updated successfully.");
                    return Ok(new APIEntityResponse<TEntity>
                    {
                        Success = true,
                        Items = updateEntity
                    });
                }

                _logger.LogWarning("Entity to update was not found.");
                return NotFound(new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "Entity to update was not found." }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the entity.");
                return StatusCode(StatusCodes.Status500InternalServerError, new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "An internal server error occurred while updating the entity." }
                });
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<APIEntityResponse<TEntity>>> Delete([FromRoute] string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                _logger.LogWarning("Delete called with null or empty ID.");
                return BadRequest(new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "ID cannot be null or empty." }
                });
            }

            try
            {
                _logger.LogInformation("Deleting entity with ID: {Id}", Id);
                bool success = false;

                if (typeof(IModelBase).IsAssignableFrom(typeof(TEntity)))
                {//si TEntity respeta la interfaz de model base
                    if (int.TryParse(Id, out var integerId))
                    {
                        success = await _repository.Delete(integerId);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid ID format for IModelBase entity.");
                    }
                }
                else
                {
                    success = await _repository.Delete(Id);
                }

                if (success)
                {
                    _logger.LogInformation("Entity with ID: {Id} deleted successfully.", Id);
                    return Ok(new APIEntityResponse<TEntity> { Success = true });
                }

                _logger.LogWarning("Entity with ID: {Id} not found for deletion.", Id);
                return NotFound(new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { $"Entity with ID {Id} was not found for deletion." }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the entity with ID: {Id}.", Id);
                return StatusCode(StatusCodes.Status500InternalServerError, new APIEntityResponse<TEntity>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "An internal server error occurred while deleting the entity." }
                });
            }
        }

        /// <summary>
        /// Helper method to extract the ID from the entity.
        /// Assumes that the entity has a property named "Id" of type string.
        /// Adjust this method based on your actual entity structure.
        /// </summary>
        private string GetEntityId(TEntity entity)
        {
            var property = typeof(TEntity).GetProperty("Id");
            return property?.GetValue(entity)?.ToString() ?? string.Empty;
        }
    }
}