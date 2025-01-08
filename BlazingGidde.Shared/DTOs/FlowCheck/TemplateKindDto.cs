using System;
using BlazingGidde.Shared.Models;

namespace BlazingGidde.Shared.DTOs.FlowCheck;

public class TemplateKindDto : IReadDto<int>
{

     public int Id { get; set; }
     
    /// <summary>
    /// The name of the template instance.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The description of the program to which it belongs.
    /// </summary>
    public string ProgramDescription { get; set; }

    /// <summary>
    /// The default number of TemplateItems associated with this template instance.
    /// </summary>
    public int DefaultItemCount { get; set; }

    /// <summary>
    /// The parent TemplateType.
    /// </summary>
    public string TemplateTypeName { get; set; }

    /// <summary>
    /// Gets or sets the iteration of the template kind. (nb of editions)
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Gets ors sets the section of the template kind.
    /// </summary>
    public string Section { get; set; }

    public TemplateCode Code { get; set; }

}
