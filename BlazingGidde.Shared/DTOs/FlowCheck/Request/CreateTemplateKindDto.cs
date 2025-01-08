using System;
using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.DTOs.FlowCheck.Request;

public class CreateTemplateKindDto : ICreateDto<int>
{
    public int Id { get; set; }

    /// <summary>
    /// The parent template type id
    /// </summary>
    public int TemplateTypeId { get; set; }


    /// <summary>
    /// The name of the template instance.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The description of the program to which it belongs.
    /// </summary>
    [Required]
    public string ProgramDescription { get; set; }

    /// <summary>
    /// Gets ors sets the section of the template kind.
    /// </summary>
    public string Section { get; set; }

    /// <summary>
    /// Gets or sets the iteration of the template kind. (nb of editions)
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Gets or sets the skip property of associated custom template items.
    /// </summary>
    public ICollection<CreateCustomTemplateItemDto> CustomTemplateItems { get; set; } = new List<CreateCustomTemplateItemDto>();

}
