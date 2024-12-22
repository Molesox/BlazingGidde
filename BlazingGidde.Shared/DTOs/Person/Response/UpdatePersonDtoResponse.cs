
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.DTOs.Person.Response;

public record UpdatePersonDtoResponse : PersonDto, IUpdateDtoResponse
{
}
