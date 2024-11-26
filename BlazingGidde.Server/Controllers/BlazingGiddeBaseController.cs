 
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;

namespace BlazingGidde.Server.Controllers
{
	public class BlazingGiddeBaseController<TEntity, TDataContext> : ControllerBase, IBlazingGiddeBaseController<TEntity>
		where TEntity : class
		where TDataContext : DbContext
	{
		protected readonly RepositoryEF<TEntity, TDataContext> _repository;
		protected readonly ILogger<BlazingGiddeBaseController<TEntity, TDataContext>> _logger;

		public BlazingGiddeBaseController(RepositoryEF<TEntity, TDataContext> repository, ILogger<BlazingGiddeBaseController<TEntity, TDataContext>> logger)
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
				var result = _repository.dbSet.ToList();

				if (result is not null)
				{
					_logger.LogInformation("Entities fetched successfully.");
					return Ok(new APIListOfEntityResponse<TEntity>()
					{
						Success = true,
						Items = result
					});
				}
				else
				{
					_logger.LogWarning("No entities found.");
					return Ok(new APIListOfEntityResponse<TEntity>()
					{
						Success = false
					});
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while fetching all entities.");
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet("{Id}")]
		public async Task<ActionResult<APIEntityResponse<TEntity>>> GetById([FromRoute] string Id)
		{
			try
			{
				_logger.LogInformation("Fetching entity with ID: {Id}", Id);
				var entity = await _repository.GetByID(int.Parse(Id));

				if (entity is not null)
				{
					_logger.LogInformation("Entity with ID: {Id} fetched successfully.", Id);
					return Ok(new APIEntityResponse<TEntity>()
					{
						Success = true,
						Items = entity
					});
				}

				_logger.LogWarning("Entity with ID: {Id} not found.", Id);
				return Ok(new APIEntityResponse<TEntity>()
				{
					Success = false
				});
			}
			catch (Exception e)
			{
				_logger.LogError(e, "An error occurred while fetching the entity with ID: {Id}.", Id);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost("getwithfilter")]
		public async Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetWithFilter([FromBody] QueryFilter<TEntity> Filter)
		{
			try
			{
				_logger.LogInformation("Fetching entities with filter.");
				var result = await _repository.Get(Filter);

				_logger.LogInformation("Entities fetched successfully with filter.");
				return Ok(new APIListOfEntityResponse<TEntity>()
				{
					Success = true,
					Items = result.ToList()
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while fetching entities with filter.");
				return StatusCode(500);
			}
		}



		[HttpPost("getwithLinqfilter")]
		public async Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetWithLinqFilter([FromBody] LinqQueryFilter<TEntity> linqQueryFilter)
		{
			try
			{
				_logger.LogInformation("Fetching entities with LINQ filter.");
				var result = await _repository.Get(linqQueryFilter);

				_logger.LogInformation("Entities fetched successfully with LINQ filter.");
				return Ok(new APIListOfEntityResponse<TEntity>()
				{
					Success = true,
					Items = result.ToList()
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while fetching entities with LINQ filter.");
				return StatusCode(500);
			}
		}

		[HttpPost]
		public async Task<ActionResult<APIEntityResponse<TEntity>>> Post([FromBody] TEntity Entity)
		{
			try
			{
				_logger.LogInformation("Inserting a new entity.");
				var insertedEntity = await _repository.Insert(Entity);

				_logger.LogInformation("Entity inserted successfully.");
				return Ok(new APIEntityResponse<TEntity>()
				{
					Success = true,
					Items = insertedEntity
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while inserting a new entity.");
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPut]
		public async Task<ActionResult<APIEntityResponse<TEntity>>> Put([FromBody] TEntity Entity)
		{
			try
			{
				_logger.LogInformation("Updating an entity.");
				var updateEntity = await _repository.Update(Entity);

				_logger.LogInformation("Entity updated successfully.");
				return Ok(new APIEntityResponse<TEntity>()
				{
					Success = true,
					Items = updateEntity
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while updating the entity.");
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete("{Id}")]
		public async Task<ActionResult<APIEntityResponse<TEntity>>> Delete([FromRoute] string Id)
		{
			try
			{
				_logger.LogInformation("Deleting entity with ID: {Id}", Id);

				var success = await _repository.Delete(int.Parse(Id));

				if (success)
				{
					_logger.LogInformation("Entity with ID: {Id} deleted successfully.", Id);
					return Ok(new APIEntityResponse<TEntity>() { Success = true });
				}

				_logger.LogWarning("Entity with ID: {Id} not found for deletion.", Id);
				return NotFound();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while deleting the entity with ID: {Id}.", Id);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
