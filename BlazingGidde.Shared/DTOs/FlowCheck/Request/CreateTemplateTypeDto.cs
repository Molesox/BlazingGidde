using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.DTOs.FlowCheck.Request;

public class CreateTemplateTypeDto : ICreateDto<int>
{
    /// <summary>
    ///     The name of the template type.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     The URL of the image associated with the template type.
    /// </summary>
    [Required]
    public string ImgUrl { get; set; } = string.Empty;

    /// <summary>
    ///     A unique 1-to-1 enum code that identifies this template type.
    /// </summary>
    public int Code { get; set; }

    [Key] public int Id { get; set; }
}