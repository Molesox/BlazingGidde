using BlazingGidde.Shared.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.Identity
{
	/// <summary>
	/// Controller for handling user account operations such as registration.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountsController"/> class.
		/// </summary>
		/// <param name="userManager">The user manager to handle identity operations.</param>
		public AccountsController(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		/// <summary>
		/// Registers a new user account.
		/// </summary>
		/// <param name="model">The registration details provided by the user.</param>
		/// <returns>An <see cref="IActionResult"/> indicating the success or failure of the registration process.</returns>
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] RegisterModel model)
		{
			var newUser = new IdentityUser { UserName = model.UserName, Email = model.Email, PhoneNumber=model.PhoneNumber, };

			var result = await _userManager.CreateAsync(newUser, model.Password!);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(x => x.Description);

				return Ok(new RegisterResult { Successful = false, Errors = errors });

			}

			return Ok(new RegisterResult { Successful = true });
		}
	}
}
