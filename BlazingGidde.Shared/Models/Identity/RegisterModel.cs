﻿using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.Models.Identity;

/// <summary>
///     Represents the model used for user registration.
/// </summary>
public class RegisterModel
{
    /// <summary>
    ///     Gets or sets the unique identifier for the user.
    /// </summary>
    [Key]
    public string? Id { get; set; }

    /// <summary>
    ///     Gets or sets the last name.
    /// </summary>
    [StringLength(80)]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the first name.
    /// </summary>
    [StringLength(80)]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the phone number for the user. This field must be in a valid phone number format.
    /// </summary>
    [Phone]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    /// <summary>
    ///     Gets or sets the email address of the user. This field is required and must be in a valid email format.
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    /// <summary>
    ///     Gets or sets the password for the user. This field is required, must be between 6 and 100 characters, and is
    ///     displayed as a password field.
    /// </summary>

    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    /// <summary>
    ///     Gets or sets the confirmation password. This field is compared with the Password field to ensure they match.
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}