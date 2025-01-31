﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.Models.Identity;

/// <summary>
///     Represents the login model used for user authentication.
/// </summary>
public class LoginModel
{
    /// <summary>
    ///     Gets or sets the email address of the user. This field is required.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the password of the user. This field is required.
    /// </summary>
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = null!;

    /// <summary>
    ///     Indicates whether the user wishes to stay logged in across sessions.
    /// </summary>
    public bool RememberMe { get; set; }
}