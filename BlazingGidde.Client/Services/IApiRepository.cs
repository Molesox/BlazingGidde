using BlazingGidde.Shared.DTOs;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Repository;

namespace BlazingGidde.Client.Services
{
    public interface IApiRepository<TEntity, Tkey, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse, TUpdateDtoResponse>
    where TEntity : class, IModelBase<Tkey>
    where TReadDto : class
    where TCreateDto : class
    where TUpdateDto : class
    where TCreateDtoResponse : class
    where TUpdateDtoResponse : class
    {
        Task<IEnumerable<TReadDto>> GetAll();


        Task<TReadDto?> GetByID(object Id);


        Task<IEnumerable<TEntity>> Get(QueryFilter<TEntity> queryFilter);


        Task<IEnumerable<TEntity>> Get(LinqQueryFilter<TEntity> linqQueryFilter);


        Task<int> GetTotalCount(LinqQueryFilter<TEntity> queryFilter);


        Task<TCreateDtoResponse> Insert( TCreateDto Entity);


        Task<TUpdateDtoResponse> Update( TUpdateDto Entity);


        Task<bool> Delete(object Id);
    }
}