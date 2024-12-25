using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.FlowCheck;

[Table("Incidency", Schema ="FlowCheck")]
public class Incidency : IModelBase<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public bool? IsQualityAdvised { get; set; }

    public bool? IsMaintenancAdvised { get; set; }

    public string Signature { get; set; } = string.Empty;

    public string CorrectiveActions { get; set; } = string.Empty;

	/// <summary>
	/// Gets or set the template parent.
	/// </summary>
	[ForeignKey(nameof(TemplateItem.Id))]
    public TemplateItem Template { get; set; }
}