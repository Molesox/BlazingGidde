//Author: DS
//Year: 2022
#nullable disable
using BlazingGidde.Shared.DTOs.Common;

namespace BlazingGidde.Shared.Repository
{
    public interface IQueryFilter<TEntity> 
    where TEntity : class
    {
        public  Task<(IEnumerable<TEntity>,int)> GetFilteredList(IQueryable<TEntity> AllItems);

        public Task<int> GetTotalCount(IQueryable<TEntity> AllItems);
    }
}
