using System;

namespace BlazingGidde.Shared.DTOs.FlowCheck.Request;

public class CreateCustomTemplateItemDto : ICreateDto<int>
{
     public int Id { get; set; }
     
    /// <summary>
    /// Gets or sets the name proprerty.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image url.
    /// </summary>
    public string? ImageUrl { get; set; }
}
