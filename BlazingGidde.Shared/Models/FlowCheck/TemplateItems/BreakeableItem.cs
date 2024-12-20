using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.FlowCheck.TemplateItems;

[Table("BreakeableItem", Schema ="FlowCheck")]
public class BreakeableItem : TemplateItem
{
    public string Name { get; set; } = string.Empty;
}
