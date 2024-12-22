using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Models.Identity;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazingGidde.Server.Data.Repository
{
    public class RepositoryRole : IRepository<FlowRole>
	{
		private readonly RoleManager<FlowRole> _roleManager;

		public RepositoryRole(RoleManager<FlowRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<bool> Delete(FlowRole entityToDelete)
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

		public async Task<IEnumerable<FlowRole>> GetAll()
		{
			return await _roleManager.Roles.ToListAsync();
		}

		public virtual IQueryable<FlowRole> GetQueryable()
		{
			return _roleManager.Roles.AsQueryable();
		}


		public async Task<FlowRole?> GetByID(object id)
		{
			return await _roleManager.FindByIdAsync(id.ToString() ?? string.Empty);
		}

		public async Task<IEnumerable<FlowRole>> Get(QueryFilter<FlowRole> queryFilter)
		{
			var all = await _roleManager.Roles.ToListAsync();
			return queryFilter.GetFilteredList(all);
		}

		public async Task<IEnumerable<FlowRole>> Get(LinqQueryFilter<FlowRole> linqQueryFilter)
		{
			return await linqQueryFilter.GetFilteredList(_roleManager.Roles);
		}

		public async Task<int> GetTotalCount(LinqQueryFilter<FlowRole> queryFilter)
		{
			return await queryFilter.GetTotalCount(_roleManager.Roles);
		}

		public async Task<FlowRole?> Insert(FlowRole entity)
		{
			var result = await _roleManager.CreateAsync(entity);
			return result.Succeeded ? entity : null;
		}

		public async Task<FlowRole?> Update(FlowRole entityToUpdate)
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
