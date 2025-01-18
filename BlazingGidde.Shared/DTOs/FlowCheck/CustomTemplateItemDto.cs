using BlazingGidde.Shared.Models.FlowCheck;
using System;

namespace BlazingGidde.Shared.DTOs.FlowCheck;

public class CustomTemplateItemDto : IReadDto<int>
{
    public int Id { get; set; }

    /// <summary>
    /// The parent template Kind id
    /// </summary>
    public int TemplateKindId { get; set; }

    /// <summary>
    /// Gets or sets the name proprerty.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image url.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The skip parent template kind property.
    /// </summary>
    public TemplateKind TemplateKind { get; set; }


}
