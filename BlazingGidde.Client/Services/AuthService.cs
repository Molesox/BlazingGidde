using BlazingGidde.Shared.Models.Identity;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace BlazingGidde.Client.Services
{
	/// <summary>
	/// Service for handling authentication-related operations such as login, registration, and logout.
	/// </summary>
	public class AuthService : IAuthService
	{
		private readonly HttpClient _httpClient;
		private readonly AuthenticationStateProvider _authenticationStateProvider;
		private readonly ILocalStorageService _localStorage;

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthService"/> class.
		/// </summary>
		/// <param name="httpClient">The HTTP client used for API calls.</param>
		/// <param name="authenticationStateProvider">The authentication state provider to manage authentication state.</param>
		/// <param name="localStorage">The local storage service for managing tokens.</param>
		public AuthService(HttpClient httpClient,
			AuthenticationStateProvider authenticationStateProvider,
			ILocalStorageService localStorage)
		{
			_httpClient = httpClient;
			_authenticationStateProvider = authenticationStateProvider;
			_localStorage = localStorage;
		}

		/// <summary>
		/// Registers a new user using the provided registration model.
		/// </summary>
		/// <param name="registerModel">The registration details.</param>
		/// <returns>A <see cref="RegisterResult"/> indicating success or failure.</returns>
		public async Task<RegisterResult> Register(RegisterModel registerModel)
		{
			var response = await _httpClient.PostAsJsonAsync("api/accounts", registerModel);
			return await response.Content.ReadFromJsonAsync<RegisterResult>();
		}

		/// <summary>
		/// Logs in a user with the provided login credentials and retrieves an authentication token.
		/// </summary>
		/// <param name="loginModel">The login credentials.</param>
		/// <returns>A <see cref="LoginResult"/> containing the success status and token, if successful.</returns>
		public async Task<LoginResult> Login(LoginModel loginModel)
		{
			var loginAsJson = JsonSerializer.Serialize(loginModel);
			var response = await _httpClient.PostAsync("api/Login",
				new StringContent(loginAsJson, Encoding.UTF8, "application/json"));

			var loginResult = JsonSerializer.Deserialize<LoginResult>(
				await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			if (!response.IsSuccessStatusCode)
			{
				return loginResult;
			}

			await _localStorage.SetItemAsync("authToken", loginResult.Token);
			((ApiAuthenticationStateProvider)_authenticationStateProvider)
				.MarkUserAsAuthenticated(loginModel.Email);

			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("bearer", loginResult.Token);

			return loginResult;
		}

		/// <summary>
		/// Logs out the current user by clearing the authentication token and updating the authentication state.
		/// </summary>
		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync("authToken");
			((ApiAuthenticationStateProvider)_authenticationStateProvider)
				.MarkUserAsLoggedOut();

			_httpClient.DefaultRequestHeaders.Authorization = null;
		}
	}
}