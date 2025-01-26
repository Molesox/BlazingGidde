using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Shared.Models.FlowCheck;

[Table("FlowRole", Schema = "FlowCheck")]
public class FlowRole : IdentityRole, IModelBase<string>
{
    public virtual ICollection<FlowUser> FlowUsers { get; set; } = new List<FlowUser>();
}