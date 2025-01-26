namespace BlazingGidde.Shared.DTOs.FlowCheck;

public class TemplateTypeDto : IReadDto<int>
{
    /// <summary>
    ///     The name of the template type.
    /// </summary>

    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     The URL of the image associated with the template type.
    /// </summary>

    public string ImgUrl { get; set; } = string.Empty;

    /// <summary>
    ///     A unique 1-to-1 enum code that identifies this template type.
    /// </summary>
    public int Code { get; set; }

    public int Id { get; set; }
}