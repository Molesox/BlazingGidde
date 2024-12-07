using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlazingGidde.Server.Data.Repository
{
	public class RepositoryRole : IRepository<IdentityRole>
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RepositoryRole(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<bool> Delete(IdentityRole entityToDelete)
		{
			var result = await _roleManager.DeleteAsync(entityToDelete);
			return result.Succeeded;
		}

		public async Task<bool> Delete(object id)
		{
			var role = await _roleManager.FindByIdAsync(id.ToString() ?? string.Empty);
			if (role == null) return false;
			var result = await _roleManager.DeleteAsync(role);
			return result.Succeeded;
		}

		public async Task<IEnumerable<IdentityRole>> GetAll()
		{
			return await _roleManager.Roles.ToListAsync();
		}

		public async Task<IdentityRole?> GetByID(object id)
		{
			return await _roleManager.FindByIdAsync(id.ToString() ?? string.Empty);
		}

		public async Task<IEnumerable<IdentityRole>> Get(QueryFilter<IdentityRole> queryFilter)
		{
			var all = await _roleManager.Roles.ToListAsync();
			return queryFilter.GetFilteredList(all);
		}

		public async Task<IEnumerable<IdentityRole>> Get(LinqQueryFilter<IdentityRole> linqQueryFilter)
		{
			return await linqQueryFilter.GetFilteredList(_roleManager.Roles);
		}

		public async Task<int> GetTotalCount(LinqQueryFilter<IdentityRole> queryFilter)
		{
			return await queryFilter.GetTotalCount(_roleManager.Roles);
		}

		public async Task<IdentityRole?> Insert(IdentityRole entity)
		{
			var result = await _roleManager.CreateAsync(entity);
			return result.Succeeded ? entity : null;
		}

		public async Task<IdentityRole?> Update(IdentityRole entityToUpdate)
		{
			var role = await _roleManager.FindByIdAsync(entityToUpdate.Id);
			if (role == null) return null;

			role.Name = entityToUpdate.Name;
			role.NormalizedName = entityToUpdate.Name?.ToUpper();

			var result = await _roleManager.UpdateAsync(role);
			return result.Succeeded ? role : null;
		}
	}
}
