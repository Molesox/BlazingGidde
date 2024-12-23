using System;
using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.DTOs.Person.Request;

public record UpdatePersonDto : CreatePersonDto, IUpdateDto<int>
{
}
