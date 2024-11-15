using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.Models.Identity
{
	/// <summary>
	/// Represents the result of a login attempt.
	/// </summary>
	public class LoginResult
	{
		/// <summary>
		/// Indicates whether the login attempt was successful.
		/// </summary>
		public bool Successful { get; set; }

		/// <summary>
		/// Gets or sets the error message if the login attempt failed. Null if no error occurred.
		/// </summary>
		public string? Error { get; set; }

		/// <summary>
		/// Gets or sets the authentication token if the login attempt was successful. Null if unsuccessful.
		/// </summary>
		public string? Token { get; set; }
	}
}
