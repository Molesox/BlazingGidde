using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.FlowCheck;

/// <summary>
/// Represents a specific timestamped instance of a TemplateType.
/// </summary>
[Table("TemplateKind", Schema ="FlowCheck")]
public class TemplateKind : ModelBase
{
    /// <summary>
    /// The name of the template instance.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The default number of TemplateItems associated with this template instance.
    /// </summary>
    public int DefaultItemCount { get; set; }

    /// <summary>
    /// The parent TemplateType.
    /// </summary>
    public TemplateType TemplateType { get; set; } = new();

    /// <summary>
    /// Gets ors sets the iteration of the template kind. (nb of editions)
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// The skip property of associated filled Templates.
    /// </summary>
    public ICollection<Template> Templates { get; set; } = new List<Template>();

    /// <summary>
    /// Gets or sets the skip property of associated custom template items.
    /// </summary>
     public ICollection<CustomTemplateItem> CustomTemplateItems { get; set; } = new List<CustomTemplateItem>();

    public TemplateCode TemplateCode { get; set; }

}
