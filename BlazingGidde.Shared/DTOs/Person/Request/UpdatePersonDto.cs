using System;
using System.ComponentModel.DataAnnotations;

namespace BlazingGidde.Shared.DTOs.Person.Request;

public record UpdatePersonDto<Tkey> : CreatePersonDto, IUpdateDto<Tkey>
{
    public Tkey Id { get; set; }
}
