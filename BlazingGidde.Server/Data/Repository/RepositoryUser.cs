using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazingGidde.Shared.Models.FlowCheck;
using AgileObjects.AgileMapper;
using AgileObjects.AgileMapper.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlazingGidde.Server.Data.Repository
{
	public class RepositoryUser : IUserRepository<FlowUser>
	{
		private readonly UserManager<FlowUser> _userManager;
		private readonly RoleManager<FlowRole> _roleManager;

		public RepositoryUser(UserManager<FlowUser> userManager, RoleManager<FlowRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
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

			var roles = await _userManager.GetRolesAsync(user);
			if (roles.Any())
			{
				var roleRemovalResult = await _userManager.RemoveFromRolesAsync(user, roles);
				if (!roleRemovalResult.Succeeded) return false;
			}

			var result = await _userManager.DeleteAsync(user);
			return result.Succeeded;
		}
		public async Task<IEnumerable<FlowUser>> GetAll()
		{
			return await _userManager.Users.ToListAsync();
		}

		public virtual IQueryable<FlowUser> GetQueryable()
		{
			return _userManager.Users.AsQueryable();
		}

		public IQueryable<FlowUser> GetByID(object id)
		{
			if (id == null)
				throw new ArgumentNullException(nameof(id));

			string idString = id.ToString() ?? string.Empty;
			return _userManager.Users.Where(user => user.Id == idString);
		}

		public IQueryable<FlowUser> Get(IQueryFilter<FlowUser> queryFilter)
		{
			return queryFilter.GetFilteredList(_userManager.Users);
		}

		public async Task<int> GetTotalCount(IQueryFilter<FlowUser> queryFilter)
		{
			return await queryFilter.GetTotalCount(_userManager.Users);
		}

		public async Task<FlowUser?> Insert(FlowUser entity)
		{
			entity.UserName = entity.Email;
			entity.Person.PersonType = null;
			var result = await _userManager.CreateAsync(entity);
			return entity;
		}

		public async Task<FlowUser?> Update(FlowUser entityToUpdate)
		{
			var currentRoles = await _userManager.GetRolesAsync(entityToUpdate);

			var existingRoles = new List<string>();
			foreach (var roleName in entityToUpdate.FlowRoles.Select(r => r.Name))
			{
				if (await _roleManager.RoleExistsAsync(roleName))
					existingRoles.Add(roleName);
			}

			entityToUpdate.FlowRoles.Clear();

			var rolesToRemove = currentRoles.Except(existingRoles).ToList();
			if (rolesToRemove.Any()) await _userManager.RemoveFromRolesAsync(entityToUpdate, rolesToRemove);

			var rolesToAdd = existingRoles.Except(currentRoles).ToList();
			if (rolesToAdd.Any()) await _userManager.AddToRolesAsync(entityToUpdate, rolesToAdd);

			await _userManager.UpdateAsync(entityToUpdate);

			return entityToUpdate;
		}
	}
}
