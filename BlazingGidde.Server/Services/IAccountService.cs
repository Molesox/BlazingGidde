using System;
using BlazingGidde.Shared.Models.Identity;

namespace BlazingGidde.Server.Services;

/// <summary>
/// Defines methods for account-related operations.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Registers a new user account asynchronously.
    /// </summary>
    /// <param name="model">The registration details provided by the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the registration result.</returns>
    Task<RegisterResult> RegisterAsync(RegisterModel model);
}
