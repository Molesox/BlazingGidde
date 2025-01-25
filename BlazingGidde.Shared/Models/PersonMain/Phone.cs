using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.PersonMain;

/// <summary>
///     The Phone model class.
/// </summary>
[Table("Phone", Schema = "Person")]
public class Phone : ModelBase
{
    #region Properties

    /// <summary>
    ///     Gets or sets the person ID.
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    ///     Gets or sets the phone type ID.
    /// </summary>
    public int PhoneTypeId { get; set; }

    /// <summary>
    ///     Gets or sets the phone number.
    /// </summary>
    [Required]
    [StringLength(30)]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the sort key.
    /// </summary>
    public int? SortKey { get; set; }

    /// <summary>
    ///     Gets or sets the remarks.
    /// </summary>
    [StringLength(200)]
    public string Remarks { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the is default flag.
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    ///     The PhoneType associated with this Phone.
    /// </summary>
    public virtual PhoneType PhoneType { get; set; }

    /// <summary>
    ///     The Person associated with this Phone.
    /// </summary>
    public virtual Person Person { get; set; }

    #endregion
}