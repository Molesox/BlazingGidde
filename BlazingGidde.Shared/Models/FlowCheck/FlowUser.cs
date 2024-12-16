using System.ComponentModel.DataAnnotations.Schema;
using BlazingGidde.Shared.Models.PersonMain;

namespace BlazingGidde.Shared.Models.FlowCheck;

[Table("FlowUser", Schema ="FlowCheck")]
public class FlowUser : ApplicationUserBase
{   
    /// <summary>
    /// The associated templates of the user.
    /// </summary>
    public ICollection<Template> Templates { get; set; } = new List<Template>();
}
