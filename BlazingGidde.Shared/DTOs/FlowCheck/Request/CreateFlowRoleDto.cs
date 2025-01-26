namespace BlazingGidde.Shared.DTOs.FlowCheck.Request;

public record CreateFlowRoleDto : ICreateDto<string>
{
    public string Name { get; set; }

    public string NormalizedName { get; set; }
    public string Id { get; set; }
}