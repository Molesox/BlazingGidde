using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.Models.Identity
{
	/// <summary>
	/// Represents the login model used for user authentication.
	/// </summary>
	public class LoginModel
	{
		/// <summary>
		/// Gets or sets the email address of the user. This field is required.
		/// </summary>
		[Required]
		public string Email { get; set; } = null!;

		/// <summary>
		/// Gets or sets the password of the user. This field is required.
		/// </summary>
		[Required]
		public string Password { get; set; } = null!;

		/// <summary>
		/// Indicates whether the user wishes to stay logged in across sessions.
		/// </summary>
		public bool RememberMe { get; set; }
	}
}
