namespace BlazingGidde.Shared.Models.FlowCheck;

public class FlowUser : ApplicationUserBase
{
    public List<Section> Sections { get; set; } = new List<Section>();

}
