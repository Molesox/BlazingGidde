namespace BlazingGidde.Shared.DTOs.FlowCheck;

public record FlowRoleDto : IReadDto<string>
{
    public string Name { get; set; }

    public string NormalizedName { get; set; }
    public string Id { get; set; }
}