using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers
{
	public interface IBlazingGiddeBaseController<TEntity>
		where TEntity : class
	{
		[HttpGet]
		Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetAll();

		[HttpGet("{Id}")]
		Task<ActionResult<APIEntityResponse<TEntity>>> GetById(string Id);

		[HttpPost("getwithfilter")]
		Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetWithFilter(QueryFilter<TEntity> Filter);

		[HttpPost("getwithLinqfilter")]
		Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetWithLinqFilter(LinqQueryFilter<TEntity> linqQueryFilter);

		[HttpPost]
		Task<ActionResult<APIEntityResponse<TEntity>>> Post([FromBody] TEntity Entity);

		[HttpPut]
		Task<ActionResult<APIEntityResponse<TEntity>>> Put([FromBody] TEntity Entity);

		[HttpDelete("{Id}")]
		Task<ActionResult<APIEntityResponse<TEntity>>> Delete(string Id);

		
	}
}