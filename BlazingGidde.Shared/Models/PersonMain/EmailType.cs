using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.PersonMain;

/// <summary>
///     The EmailType model class.
/// </summary>
[Table("EmailType", Schema = "Person")]
public class EmailType : ModelBase
{
    #region Properties

    /// <summary>
    ///     Gets or sets the code.
    /// </summary>
    [Required]
    [StringLength(2)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the sort key.
    /// </summary>

    public int? SortKey { get; set; }

    /// <summary>
    ///     The collection of Emails associated with this Email type.
    /// </summary>
    [ForeignKey("EmailTypeId")]
    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    #endregion
}