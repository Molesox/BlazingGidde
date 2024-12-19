using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.FlowCheck;

[Table("CustomTemplateItem", Schema ="FlowCheck")]
public class CustomTemplateItem 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name proprerty.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image url.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The skip parent template kind property.
    /// </summary>
    public TemplateCode TemplateCodeKind { get; set; } = new();
}