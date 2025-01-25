using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazingGidde.Client;

/// <summary>
///     Provides an implementation of <see cref="AuthenticationStateProvider" /> that uses an API to manage authentication
///     state.
/// </summary>
public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiAuthenticationStateProvider" /> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client used for API requests.</param>
    /// <param name="localStorage">The local storage service to manage authentication tokens.</param>
    public ApiAuthenticationStateProvider(HttpClient httpClient,
        ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    /// <summary>
    ///     Retrieves the current authentication state, including user claims if authenticated.
    /// </summary>
    /// <returns>An <see cref="AuthenticationState" /> representing the current user.</returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Depending on what this method returns, Authorise or not authorised view will be displayed (among other usages)
        var savedToken = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrWhiteSpace(savedToken))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        // includes token in all requests.
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
    }

    /// <summary>
    ///     Marks the user as authenticated and notifies of the state change.
    /// </summary>
    /// <param name="email">The email of the authenticated user.</param>
    public async void MarkUserAsAuthenticated(string email)
    {
        var savedToken = await _localStorage.GetItemAsync<string>("authToken");

        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "apiauth"));

        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    /// <summary>
    ///     Marks the user as logged out and notifies of the state change.
    /// </summary>
    public void MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }

    /// <summary>
    ///     Parses the claims from a JSON Web Token (JWT).
    /// </summary>
    /// <param name="jwt">The JWT string.</param>
    /// <returns>An enumerable of claims extracted from the token.</returns>
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        keyValuePairs!.TryGetValue(ClaimTypes.Role, out var roles);

        if (roles is not null)
        {
            if (roles.ToString()!.Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);

                foreach (var parsedRole in parsedRoles!) claims.Add(new Claim(ClaimTypes.Role, parsedRole));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
            }

            keyValuePairs.Remove(ClaimTypes.Role);
        }

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));

        return claims;
    }

    /// <summary>
    ///     Parses a Base64 string, adding any required padding to make it valid.
    /// </summary>
    /// <param name="base64">The Base64 string to parse.</param>
    /// <returns>A byte array of the parsed Base64 string.</returns>
    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}