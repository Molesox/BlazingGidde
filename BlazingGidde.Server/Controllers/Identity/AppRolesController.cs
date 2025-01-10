using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Server.Data;
using BlazingGidde.Shared.Models.Identity;
using BlazingGidde.Shared.Repository;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.DTOs.FlowCheck;
using BlazingGidde.Shared.DTOs.FlowCheck.Request;

namespace BlazingGidde.Server.Controllers.Identity
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AppRolesController : OnaBaseController<FlowRole, string, ApplicationDbContext, FlowRoleDto, CreateFlowRoleDto>
    {
        public AppRolesController(IRepository<FlowRole> repository, 
            ILogger<OnaBaseController<FlowRole, string, ApplicationDbContext, FlowRoleDto, CreateFlowRoleDto, CreateFlowRoleDto, FlowRoleDto, FlowRoleDto>> logger) 
            : base(repository, logger)
        {
        }
    }
}

