using System;
using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Shared.Models.FlowCheck;

public class FlowUser : IdentityUser
{
    public List<Section> Sections { get; set; } = new List<Section>();

}
