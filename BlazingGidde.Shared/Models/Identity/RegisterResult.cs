using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.Models.Identity
{

 
	/// <summary>
	/// Represents the result of a user registration attempt.
	/// </summary>
	public class RegisterResult
	{
		/// <summary>
		/// Indicates whether the registration attempt was successful.
		/// </summary>
		public bool Successful { get; set; }

		/// <summary>
		/// Gets or sets a collection of error messages if the registration attempt failed. Null if no errors occurred.
		/// </summary>
		public IEnumerable<string>? Errors { get; set; }
	}
}
