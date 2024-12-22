
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.DTOs.Person.Request;

public record CreatePersonDto : ICreateDto
{
    public int PersonTypeId { get; set; }

    [Required]
    [StringLength(20)]
    public string Culture { get; set; }

    [StringLength(80)]
    public string Title { get; set; }

    [StringLength(80)]
    public string LastName { get; set; }

    [StringLength(80)]
    public string FirstName { get; set; }

    [StringLength(200)]
    public string? Remarks { get; set; }
}
