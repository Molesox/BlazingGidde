using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.DTOs.FlowCheck.Request
{
    public record CreateFlowRoleDto : ICreateDto<string>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
