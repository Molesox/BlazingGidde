using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingGidde.Server.Controllers.Identity
{
	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class AppUsersController : ControllerBase, IBlazingGiddeBaseController<IdentityUser>
	{
		private readonly UserManager<IdentityUser> _userManager;

		public AppUsersController(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<ActionResult<APIListOfEntityResponse<IdentityUser>>> GetAll()
		{
			var users = await _userManager.Users.ToListAsync();
			return Ok(new APIListOfEntityResponse<IdentityUser>()
			{
				Success = true,
				Data = users
			});
		}

		[HttpGet("{Id}")]
		public async Task<ActionResult<APIEntityResponse<IdentityUser>>> GetById(string Id)
		{
			var user = await _userManager.FindByIdAsync(Id);

			if (user == null)
			{
				return NotFound(new APIEntityResponse<IdentityUser>
				{
					Success = false,
					ErrorMessages = [$"User with Id {Id} not found."]
				});
			}

			return Ok(new APIEntityResponse<IdentityUser>
			{
				Success = true,
				Data = user
			});
		}

		[HttpPost("getwithfilter")]
		public Task<ActionResult<APIListOfEntityResponse<IdentityUser>>> GetWithFilter(QueryFilter<IdentityUser> Filter)
		{
			throw new NotImplementedException();
		}

		[HttpPost("getwithLinqfilter")]
		public async Task<ActionResult<APIListOfEntityResponse<IdentityUser>>> GetWithLinqFilter(LinqQueryFilter<IdentityUser> linqQueryFilter)
		{
			var users = await linqQueryFilter.GetFilteredList(_userManager.Users.AsQueryable());

			return Ok(new APIListOfEntityResponse<IdentityUser>
			{
				Success = true,
				Data = users
			});
		}

		[HttpPost]
		public async Task<ActionResult<APIEntityResponse<IdentityUser>>> Post(IdentityUser Entity)
		{
			var result = await _userManager.CreateAsync(Entity);

			if (!result.Succeeded)
			{
				return BadRequest(new APIEntityResponse<IdentityUser>
				{
					Success = false,
					ErrorMessages = result.Errors.Select(e => e.Description).ToList()
				});
			}

			return Ok(new APIEntityResponse<IdentityUser>
			{
				Success = true,
				Data = Entity
			});
		}

		[HttpPut]
		public async Task<ActionResult<APIEntityResponse<IdentityUser>>> Put(IdentityUser Entity)
		{
			var user = await _userManager.FindByIdAsync(Entity.Id);

			if (user == null)
			{
				return NotFound(new APIEntityResponse<IdentityUser>
				{
					Success = false,
					ErrorMessages = [$"User with Id {Entity.Id} not found."]
				});
			}

			user.Email = Entity.Email;
			user.UserName = Entity.UserName;

			var result = await _userManager.UpdateAsync(user);

			if (!result.Succeeded)
			{
				return BadRequest(new APIEntityResponse<IdentityUser>
				{
					Success = false,
					ErrorMessages = result.Errors.Select(e => e.Description).ToList()
				});
			}

			return Ok(new APIEntityResponse<IdentityUser>
			{
				Success = true,
				Data = user
			});
		}

		[HttpDelete("{Id}")]
		public async Task<ActionResult> Delete(string Id)
		{
			var user = await _userManager.FindByIdAsync(Id);

			if (user == null)
			{
				return NotFound(new APIEntityResponse<IdentityUser>
				{
					Success = false,
					ErrorMessages = [$"User with Id {Id} not found."]
				});
			}

			var result = await _userManager.DeleteAsync(user);

			if (!result.Succeeded)
			{
				return BadRequest(new APIEntityResponse<IdentityUser>
				{
					Success = false,
					ErrorMessages = result.Errors.Select(e => e.Description).ToList()
				});
			}

			return NoContent();
		}
	}
}