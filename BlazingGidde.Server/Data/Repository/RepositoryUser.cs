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
		private readonly IRepository<FlowUser> _userRepository;

		public RepositoryUser(UserManager<FlowUser> userManager, IRepository<FlowUser> userRepository)
		{
			_userManager = userManager;
			_userRepository = userRepository;
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

			var identityUser = await _userManager.FindByIdAsync(entityToUpdate.Id);
			if (identityUser == null) return null;

			identityUser.UserName = entityToUpdate.UserName;
			identityUser.Email = entityToUpdate.Email;

			var result = await _userManager.UpdateAsync(identityUser);

			if (!result.Succeeded) return null;
			var person = _userRepository.GetByID(entityToUpdate.Id).First();
			if (person != null)
			{
				person = entityToUpdate.Map().Over(person);

				return await _userRepository.Update(person);
			}
			return null;
		}
	}
}
