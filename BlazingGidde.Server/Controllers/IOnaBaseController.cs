using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs;
using BlazingGidde.Shared.DTOs.Common;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Repository;
using DevExpress.Blazor.Internal;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers
{
	public interface IOnaBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse, TUpdateDtoReponse>
		where TEntity : class, IModelBase<Tkey>
		where Tkey : IEquatable<Tkey>
        where TReadDto : class
        where TCreateDto : class
        where TUpdateDto : class, IModelBase<Tkey>
        where TCreateDtoResponse : class
        where TUpdateDtoReponse : class
	{
		Task<object> Get(DataSourceLoadOptions loadOptions);
		
		Task<ActionResult<APIListOfEntityResponse<TReadDto>>> GetAll();

		Task<ActionResult<APIEntityResponse<TReadDto>>> GetById(Tkey Id);

		Task<ActionResult<APIEntityResponse<QueryFilterResponse<TReadDto>>>> GetWithFilter(QueryFilter<TEntity> queryFilter);

		Task<ActionResult<APIEntityResponse<QueryFilterResponse<TReadDto>>>> GetWithLinqFilter(LinqQueryFilter<TEntity> linqQueryFilter);

		Task<ActionResult<APIEntityResponse<CountDto>>> GetTotalCount(LinqQueryFilter<TEntity> linqQueryFilter);
		Task<ActionResult<APIEntityResponse<CountDto>>> GetTotalCount(QueryFilter<TEntity> queryFilter);

		Task<ActionResult<APIEntityResponse<TCreateDtoResponse>>> Post([FromBody] TCreateDto Entity);

		Task<ActionResult<APIEntityResponse<TUpdateDtoReponse>>> Put([FromBody] TUpdateDto Entity);

		Task<ActionResult<APIEntityResponse<TEntity>>> Delete(Tkey Id);
		
	}
}