//Author: DS
//Year: 2022
// 1
namespace BlazingGidde.Shared.Repository
{
	/// <summary>
	/// The IRepository interface provides the standard operations to be performed on a data repository for a given type.
	/// </summary>
	/// <typeparam name="TEntity">Type of entity that this repository works with.</typeparam>
	public interface IUserRepository<TEntity> : IRepository<TEntity>
	 where TEntity : class
	{
		
	}
}
