
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.DTOs.Person.Response;

public record CreatePersonDtoResponse : PersonDto, ICreateDtoResponse
{
}
