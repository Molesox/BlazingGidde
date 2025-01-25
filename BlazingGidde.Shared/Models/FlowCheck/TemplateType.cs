using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazingGidde.Shared.Repository;

namespace BlazingGidde.Shared.Models.FlowCheck;

[Table("TemplateType", Schema = "FlowCheck")]
public class TemplateType : ModelBase, ISupportType
{
    /// <summary>
    ///     The name of the template type.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     The URL of the image associated with the template type.
    /// </summary>
    [Required]
    public string ImgUrl { get; set; } = string.Empty;

    /// <summary>
    ///     A collection of associated template kinds with their skip property.
    /// </summary>
    public ICollection<TemplateKind> TemplateKinds { get; set; } = new List<TemplateKind>();

    /// <summary>
    ///     A unique 1-to-1 enum code that identifies this template type.
    /// </summary>
    public int Code { get; set; }
}