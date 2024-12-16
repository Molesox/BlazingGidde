
namespace BlazingGidde.Shared.API;
	public class APIResponse
	{
		#region Properties

		/// <summary>
		/// Gets or sets a value indicating whether the API call was successful.
		/// </summary>
		public bool Success { get; set; }

		/// <summary>
		/// Gets or sets the error messages, if any, resulting from the API call.
		/// </summary>
		public List<string> ErrorMessages { get; set; } = new List<string>();

		#endregion
	}
