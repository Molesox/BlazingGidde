using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazingGidde.Client.Services
{
	public class UserRoleManager
	{
		private readonly HttpClient _httpClient;
		private const string ControllerName = "userrole";

		public UserRoleManager(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<APIResponse> AssignRole(AssignRoleDto dto)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/assign-role", dto);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<APIResponse>() ?? new APIResponse { Success = false };
			}
			catch (Exception)
			{
				return new APIResponse { Success = false, ErrorMessages = new List<string> { "An error occurred while assigning the role." } };
			}
		}

		public async Task<APIResponse> RemoveRole(AssignRoleDto dto)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/remove-role", dto);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<APIResponse>() ?? new APIResponse { Success = false };
			}
			catch (Exception)
			{
				return new APIResponse { Success = false, ErrorMessages = new List<string> { "An error occurred while removing the role." } };
			}
		}

		public async Task<APIListOfEntityResponse<string>> GetRoles(string userId)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ControllerName}/{userId}/roles");
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<APIListOfEntityResponse<string>>()
					   ?? new APIListOfEntityResponse<string> { Success = false };
			}
			catch (Exception)
			{
				return new APIListOfEntityResponse<string>
				{
					Success = false,
					ErrorMessages = new List<string> { "An error occurred while fetching roles." }
				};
			}
		}
	}
}
