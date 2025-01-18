using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Shared.Models.FlowCheck;

[Table("FlowUser", Schema = "FlowCheck")]
public class FlowUser : IdentityUser, IModelBase<string>
{

	
	/// <summary>
	/// The associated templates of the user.
	/// </summary>
	public ICollection<Template> Templates { get; set; } = new List<Template>();


	public virtual ICollection<FlowRole> FlowRoles { get; set; } = new List<FlowRole>();

	#region Interface implementation

	public Person Person { get; set; }

	#region Auditables

	public DateTime? UpdateDate { get; set; }

	[Required]
	public DateTime CreateDate { get; set; }

	[Required]
	[MaxLength(256)]
	public string CreateUser { get; set; } = string.Empty;

	[MaxLength(256)]
	public string? UpdateUser { get; set; }

	#endregion

	#endregion
}
