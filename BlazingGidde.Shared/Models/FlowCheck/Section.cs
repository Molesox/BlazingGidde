namespace BlazingGidde.Shared.Models.FlowCheck;

public class Section : ModelBase
{
    public string Name { get; set; } = string.Empty;

    public List<Template> Templates { get; set; } = new List<Template>();
    public List<FlowUser> FlowUsers { get; set; } = new List<FlowUser>();
}