using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlazingGidde.Server.Data.Repository
{
	public class RepositoryUser : IRepository<IdentityUser>
	{
		private readonly UserManager<IdentityUser> _userManager;

		public RepositoryUser(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<bool> Delete(IdentityUser entityToDelete)
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

		public async Task<IEnumerable<IdentityUser>> GetAll()
		{
			return await _userManager.Users.ToListAsync();
		}

		public async Task<IdentityUser?> GetByID(object id)
		{
			return await _userManager.FindByIdAsync(id.ToString() ?? string.Empty);
		}

		public async Task<IEnumerable<IdentityUser>> Get(QueryFilter<IdentityUser> queryFilter)
		{
			var all = await _userManager.Users.ToListAsync();
			return queryFilter.GetFilteredList(all);
		}

		public async Task<IEnumerable<IdentityUser>> Get(LinqQueryFilter<IdentityUser> linqQueryFilter)
		{
			return await linqQueryFilter.GetFilteredList(_userManager.Users);
		}

		public async Task<int> GetTotalCount(LinqQueryFilter<IdentityUser> queryFilter)
		{
			return await queryFilter.GetTotalCount(_userManager.Users);
		}

		public async Task<IdentityUser?> Insert(IdentityUser entity)
		{
			var result = await _userManager.CreateAsync(entity);
			return result.Succeeded ? entity : null;
		}

		public async Task<IdentityUser?> Update(IdentityUser entityToUpdate)
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
