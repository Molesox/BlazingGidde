using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.FlowCheck;

[Table("TemplateType", Schema ="FlowCheck")]
public class TemplateType : ModelBase
{
    /// <summary>
    /// The name of the template type.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The URL of the image associated with the template type.
    /// </summary>
    [Required]
    public string ImgUrl { get; set; } = string.Empty;

    /// <summary>
    /// A unique 1-to-1 code that identifies this template type.
    /// </summary>
    public TemplateCode Code { get; set; }

    /// <summary>
    /// A collection of associated template kinds with their skip property.
    /// </summary>
    public ICollection<TemplateKind> TemplateKinds { get; set; } = new List<TemplateKind>();
}