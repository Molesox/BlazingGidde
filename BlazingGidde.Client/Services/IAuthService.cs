using BlazingGidde.Shared.Models.Identity;

namespace BlazingGidde.Client.Services;

/// <summary>
///     Defines the contract for authentication services, including login, logout, and registration.
/// </summary>
public interface IAuthService
{
    /// <summary>
    ///     Logs in a user with the specified credentials.
    /// </summary>
    /// <param name="loginModel">The login credentials.</param>
    /// <returns>A <see cref="LoginResult" /> containing the success status and token, if successful.</returns>
    Task<LoginResult> Login(LoginModel loginModel);

    /// <summary>
    ///     Logs out the current user by clearing authentication tokens and updating the authentication state.
    /// </summary>
    Task Logout();

    /// <summary>
    ///     Registers a new user with the specified registration details.
    /// </summary>
    /// <param name="registerModel">The registration details.</param>
    /// <returns>A <see cref="RegisterResult" /> indicating the success or failure of the registration process.</returns>
    Task<RegisterResult> Register(RegisterModel registerModel);
}