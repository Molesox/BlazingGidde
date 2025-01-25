using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.PersonMain;

/// <summary>
///     The AddressType model class.
/// </summary>
[Table("AddressType", Schema = "Person")]
public class AddressType : ModelBase
{
    #region Properties

    /// <summary>
    ///     Gets or sets the Code.
    /// </summary>
    [Required]
    [StringLength(1)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the Name.
    /// </summary>
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the SortKey.
    /// </summary>
    public int? SortKey { get; set; }

    /// <summary>
    ///     The collection of Addresses associated with this Address type.
    /// </summary>
    [ForeignKey("AddressTypeId")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    #endregion
}