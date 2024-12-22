using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Shared.Models.FlowCheck
{
    [Table("FlowRole", Schema = "FlowCheck")]
    public class FlowRole : IdentityRole, IModelBase<string>
    {

    }
}