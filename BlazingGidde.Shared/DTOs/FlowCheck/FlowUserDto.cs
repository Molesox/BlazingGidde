using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Shared.DTOs.FlowCheck;

public record FlowUserDto : IReadDto<string>
{
    public int PersonId { get; set; }

    public int PersonPersonTypeId { get; set; }

    public string PersonFirstName { get; set; }

    public string PersonLastName { get; set; }

    public string PersonFullName => $"{PersonLastName}, {PersonFirstName}";

    public string PersonTitle { get; set; }

    public string PersonCulture { get; set; }

    public string PersonPhonesPhoneNumber { get; set; }
    public int PersonPhonesPhoneTypeId { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    /// <summary>
    ///     Gets or sets a flag indicating if a user has confirmed their telephone address.
    /// </summary>
    /// <value>True if the telephone number has been confirmed, otherwise false.</value>
    [PersonalData]
    public virtual bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    ///     Gets or sets a flag indicating if two factor authentication is enabled for this user.
    /// </summary>
    /// <value>True if 2fa is enabled, otherwise false.</value>
    [PersonalData]
    public virtual bool TwoFactorEnabled { get; set; }

    /// <summary>
    ///     Gets or sets the date and time, in UTC, when any user lockout ends.
    /// </summary>
    /// <remarks>
    ///     A value in the past means the user is not locked out.
    /// </remarks>
    public virtual DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    ///     Gets or sets a flag indicating if the user could be locked out.
    /// </summary>
    /// <value>True if the user could be locked out, otherwise false.</value>
    public virtual bool LockoutEnabled { get; set; }

    public string Id { get; set; }
}