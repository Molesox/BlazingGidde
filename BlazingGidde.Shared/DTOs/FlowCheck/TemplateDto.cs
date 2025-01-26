using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Shared.DTOs.FlowCheck;

public class TemplateDto : IReadDto<int>
{
    public DateTime? UpdateDate { get; set; }

    [Required] public DateTime CreateDate { get; set; }

    [Required] [MaxLength(256)] public string CreateUser { get; set; } = string.Empty;

    [MaxLength(256)] public string? UpdateUser { get; set; }

    public int TemplateKindId { get; set; }

    public List<TemplateItem>? TemplateItems { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}