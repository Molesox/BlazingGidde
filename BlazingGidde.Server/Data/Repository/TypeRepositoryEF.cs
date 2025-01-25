using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlazingGidde.Server.Data.Repository;

public class TypeRepositoryEF<TEntity, TDataContext> : RepositoryEF<TEntity, TDataContext, int>,
    ITypeRepositoryEF<TEntity>
    where TEntity : class, IModelBase<int>, ISupportType
    where TDataContext : DbContext
{
    public TypeRepositoryEF(TDataContext dataContext) : base(dataContext)
    {
    }

    /// <summary>
    ///     Gets an entity using its Code.
    /// </summary>
    /// <param name="code">Code of the entity to retrieve.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public virtual async Task<TEntity?> GetByCode(int code)
    {
        return await dbSet.FirstAsync(type => type.Code == code);
    }
}