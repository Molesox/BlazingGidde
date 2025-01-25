using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.FlowCheck;

/// <summary>
///     Get or sets the control step.
/// </summary>
[Table("TemplateItem", Schema = "FlowCheck")]
public class TemplateItem : ModelBase
{
    /// <summary>
    ///     The parent template id
    /// </summary>
    public int TemplateId { get; set; }

    /// <summary>
    ///     Gets or sets the success status of the control item.
    /// </summary>
    public bool? Success { get; set; }

    public string? Traceability { get; set; } = string.Empty;

    public string? Line { get; set; }

    /// <summary>
    ///     Gets or sets the parent template.
    /// </summary>
    public Template Template { get; set; }

    /// <summary>
    ///     Gets or sets the possible related incidency.
    /// </summary>
    public Incidency? Incidency { get; set; }
}