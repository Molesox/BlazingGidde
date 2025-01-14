using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace BlazingGidde.Shared.DTOs.FlowCheck.Request;

public class UpdateTemplateKindDto : CreateTemplateKindDto, IUpdateDto<int>
{
}
