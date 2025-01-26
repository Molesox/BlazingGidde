namespace BlazingGidde.Shared.Repository;

public interface ITypeRepositoryEF<TEntity> : IRepository<TEntity>
    where TEntity : class, ISupportType
{
    Task<TEntity?> GetByCode(int code);
}