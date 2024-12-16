using System;
using System.ComponentModel.DataAnnotations;
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Shared.Models;

/// <summary>
/// This base class is not intended to be in db.
/// </summary>
public class ApplicationUserBase : IdentityUser, ISupportTimeStamp
{
    public Person? Person { get; set; }

    #region Auditables

    public DateTime? UpdateDate { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    [MaxLength(256)]
    public string CreateUser { get; set; } = string.Empty;

    [MaxLength(256)]
    public string? UpdateUser { get; set; }

    #endregion
}
