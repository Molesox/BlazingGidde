using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Server.Data.Repository
{
    public class RepositoryUser : IRepository<FlowUser>
	{
		private readonly UserManager<FlowUser> _userManager;

		public RepositoryUser(UserManager<FlowUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<bool> Delete(FlowUser entityToDelete)
		{
			var result = await _userManager.DeleteAsync(entityToDelete);
			return result.Succeeded;
		}

		public async Task<bool> Delete(object id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString() ?? string.Empty);
			if (user == null) return false;

			var result = await _userManager.DeleteAsync(user);
			return result.Succeeded;
		}

		public async Task<IEnumerable<FlowUser>> GetAll()
		{
			return await _userManager.Users.ToListAsync();
		}

		public virtual IQueryable<FlowUser> GetAllQueryable()
		{
			return _userManager.Users.AsQueryable();
		}

		public async Task<FlowUser?> GetByID(object id)
		{
			return await _userManager.FindByIdAsync(id.ToString() ?? string.Empty);
		}

		public async Task<IEnumerable<FlowUser>> Get(QueryFilter<FlowUser> queryFilter)
		{
			var all = await _userManager.Users.ToListAsync();
			return queryFilter.GetFilteredList(all);
		}

		public async Task<IEnumerable<FlowUser>> Get(LinqQueryFilter<FlowUser> linqQueryFilter)
		{
			return await linqQueryFilter.GetFilteredList(_userManager.Users);
		}

		public async Task<int> GetTotalCount(LinqQueryFilter<FlowUser> queryFilter)
		{
			return await queryFilter.GetTotalCount(_userManager.Users);
		}

		public async Task<FlowUser?> Insert(FlowUser entity)
		{
			var result = await _userManager.CreateAsync(entity);
			return result.Succeeded ? entity : null;
		}

		public async Task<FlowUser?> Update(FlowUser entityToUpdate)
		{
			var user = await _userManager.FindByIdAsync(entityToUpdate.Id);
			if (user == null) return null;

			user.Email = entityToUpdate.Email;
			user.UserName = entityToUpdate.UserName;
			user.PhoneNumber = entityToUpdate.PhoneNumber;

			var result = await _userManager.UpdateAsync(user);
			return result.Succeeded ? user : null;
		}
	}
}
