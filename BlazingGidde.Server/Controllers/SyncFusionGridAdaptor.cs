using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;

namespace BlazingGidde.Server.Controllers
{
    [Route("api/Controller")]
	public class SyncFusionGridAdaptor<TEntity, TDataContext> : ControllerBase
		where TEntity : class
		where TDataContext : DbContext
	{
		protected readonly RepositoryEF<TEntity, TDataContext> _repository;
		protected readonly ILogger<SyncFusionGridAdaptor<TEntity, TDataContext>> _logger;

		public SyncFusionGridAdaptor(RepositoryEF<TEntity, TDataContext> repository, ILogger<SyncFusionGridAdaptor<TEntity, TDataContext>> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpGet]
   
		public List<TEntity> GetAll()
		{
			try
			{
				_logger.LogInformation("Fetching all entities.");
				var result = _repository.dbSet.ToList();

				if (result is not null)
				{
					_logger.LogInformation("Entities fetched successfully.");

                    return result;
				 
				}
				else
				{
					_logger.LogWarning("No entities found.");

                    return result;
				 
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while fetching all entities.");
				return new List<TEntity>();
			}
		}
        [HttpPost]
 
        public object Post([FromBody] DataManagerRequest DataManagerRequest)
        {
            var DataSource = _repository.dbSet.AsQueryable();
            int TotalRecordsCount = DataSource.Cast<TEntity>().Count();
            // Handling Paging in UrlAdaptor.
            if (DataManagerRequest.Skip != 0)
            {
                // Paging
                DataSource = DataOperations.PerformSkip(DataSource, DataManagerRequest.Skip);
                //Add custom logic here if needed and remove above method
            }
            if (DataManagerRequest.Take != 0)
            {
                DataSource = DataOperations.PerformTake(DataSource, DataManagerRequest.Take);
                //Add custom logic here if needed and remove above method
            }
            return new { result = DataSource, count = TotalRecordsCount };
        }
 
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

		[HttpPost]
        [Route("api/[Controller]/Insert")]
		public void  Post([FromBody] TEntity Entity)
		{
			try
			{
				_logger.LogInformation("Inserting a new entity.");
				var insertedEntity =  _repository.Insert(Entity);

				_logger.LogInformation("Entity inserted successfully.");
			 
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while inserting a new entity.");
			}
		}

		[HttpPost]
		[Route("api/[Controller]/Update")]
		public void Put([FromBody] TEntity Entity)
		{
			try
			{
				_logger.LogInformation("Updating an entity.");
			    _repository.Update(Entity).RunSynchronously();

				_logger.LogInformation("Entity updated successfully.");
				 
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while updating the entity.");
			 
			}
		}

 
		[HttpPost]
		[Route("api/[Controller]/Delete")]
		public void Delete([FromRoute] TEntity Id)
		{
			try
			{
				_logger.LogInformation("Deleting entity with ID: {Id}", Id);

				_repository.Delete(Id).RunSynchronously();

			 
					_logger.LogInformation("Entity with ID: {Id} deleted successfully.", Id);
					 
				 

				 
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while deleting the entity with ID: {Id}.", Id);
				 
			}
		}
	}
}
