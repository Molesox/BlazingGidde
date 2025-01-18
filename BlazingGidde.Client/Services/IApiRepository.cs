using BlazingGidde.Shared.DTOs;
using BlazingGidde.Shared.DTOs.Common;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Repository;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services
{
    public interface IApiRepository<TEntity, Tkey, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse, TUpdateDtoResponse>
    where TEntity : class, IModelBase<Tkey>
    where TReadDto : class
    where Tkey : IEquatable<Tkey>
    where TCreateDto : class, IModelBase<Tkey>
    where TUpdateDto : class, IModelBase<Tkey>
    where TCreateDtoResponse : class
    where TUpdateDtoResponse : class
    {
        Task<IEnumerable<TReadDto>> GetAll();

        Task<TReadDto?> GetByID(object Id);

        Task<TCreateDto?> GetEditModelByID(object id);

        GridDevExtremeDataSource<TReadDto> Get();

        Task<QueryFilterResponse<TReadDto>> Get(QueryFilter<TEntity> queryFilter);

        Task<QueryFilterResponse<TReadDto>> Get(LinqQueryFilter<TEntity> linqQueryFilter);

        Task<int> GetTotalCount(QueryFilter<TEntity> queryFilter);

        Task<int> GetTotalCount(LinqQueryFilter<TEntity> linqQueryFilter);

        Task<TCreateDtoResponse> Insert(TCreateDto Entity);

        Task<TUpdateDtoResponse> Update(TUpdateDto Entity);

        Task<bool> Delete(object Id);
    }
}