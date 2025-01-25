//Author: DS
//Year: 2022

#nullable disable
namespace BlazingGidde.Shared.Repository;

public interface IQueryFilter<TEntity>
    where TEntity : class
{
    public IQueryable<TEntity> GetFilteredList(IQueryable<TEntity> AllItems);

    public Task<int> GetTotalCount(IQueryable<TEntity> AllItems);
}