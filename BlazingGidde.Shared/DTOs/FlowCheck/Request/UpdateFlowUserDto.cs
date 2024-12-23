using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.DTOs.FlowCheck.Request
{
	public record UpdateFlowUserDto : CreateFlowUserDto, IUpdateDto<string>
	{
	}
}
