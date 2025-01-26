using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.DTOs.FlowCheck.Request;

public record CreateFlowUserDto : ICreateDto<string>
{
    public int PersonId { get; set; }

    public int PersonPersonTypeId { get; set; }

    // [Required]
    [StringLength(20)] public string PersonCulture { get; set; }

    [StringLength(80)]
    // [Required]
    public string PersonTitle { get; set; }

    [StringLength(80)] [Required] public string PersonLastName { get; set; }

    [StringLength(80)] [Required] public string PersonFirstName { get; set; }

    [EmailAddress] [Required] public string Email { get; set; }

    [Phone] [Required] public string PhoneNumber { get; set; }

    public ICollection<NameFlowRoleDto> FlowRoles { get; set; } = new List<NameFlowRoleDto>();
    public string Id { get; set; }
}