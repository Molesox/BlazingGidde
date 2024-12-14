using System;
using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.Models.FlowCheck;

/// <summary>
/// Represents a specific timestamped instance of a TemplateType.
/// </summary>
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
    public required TemplateType TemplateType { get; set; }

    /// <summary>
    /// Gets ors sets the iteration of the template kind. (nb of editions)
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// The skip property of associated filled Templates.
    /// </summary>
    public ICollection<Template> Templates { get; set; } = new List<Template>();
}