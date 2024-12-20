using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.FlowCheck;

/// <summary>
/// Represents a record of a certain TemplateKind.
/// </summary>
[Table("Template", Schema ="FlowCheck")]
public class Template : ModelBase
{
    
    /// <summary>
    /// Gets or sets the skip property of associated template items.
    /// </summary>
    public List<TemplateItem>? TemplateItems {get; set;}

}
