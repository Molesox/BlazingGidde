using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlazingGidde.Shared.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlazingGidde.Server.Controllers.Identity
{
	/// <summary>
	/// Controller for handling user login operations.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly SignInManager<IdentityUser> _signInManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoginController"/> class.
		/// </summary>
		/// <param name="configuration">The configuration to retrieve JWT settings.</param>
		/// <param name="signInManager">The sign-in manager to handle user authentication.</param>
		public LoginController(IConfiguration configuration,
			SignInManager<IdentityUser> signInManager)
		{
			_configuration = configuration;
			_signInManager = signInManager;
		}

		/// <summary>
		/// Authenticates a user and returns a JWT token if successful.
		/// </summary>
		/// <param name="login">The login credentials provided by the user.</param>
		/// <returns>An <see cref="IActionResult"/> with a success status and token or error details.</returns>
		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginModel login)
		{
			var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

			// if (!result.Succeeded)
			// {
			// 	return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });
			// }


			var claims = new[]
			{
				new Claim(ClaimTypes.Name, login.Email)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]!));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

			var token = new JwtSecurityToken(
				_configuration["JwtIssuer"],
				_configuration["JwtAudience"],
				claims,
				expires: expiry,
				signingCredentials: creds
			);

			return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
		}
	}
}