using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.FlowCheck;

/// <summary>
///     Represents a record of a certain TemplateKind.
/// </summary>
[Table("Template", Schema = "FlowCheck")]
public class Template : ModelBase
{
    /// <summary>
    ///     The parent template kind id
    /// </summary>
    public int TemplateKindId { get; set; }

    /// <summary>
    ///     Gets or sets the skip property of associated template items.
    /// </summary>
    public List<TemplateItem>? TemplateItems { get; set; }
}