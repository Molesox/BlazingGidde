using BlazingGidde.Server.Data;
using BlazingGidde.Shared.DTOs.FlowCheck;
using BlazingGidde.Shared.DTOs.FlowCheck.Request;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.Identity;

[ApiController]
[Route("[controller]")]
[Authorize]
public class
    AppRolesController : OnaBaseController<FlowRole, string, ApplicationDbContext, FlowRoleDto, CreateFlowRoleDto>
{
    public AppRolesController(IRepository<FlowRole> repository,
        ILogger<OnaBaseController<FlowRole, string, ApplicationDbContext, FlowRoleDto, CreateFlowRoleDto,
            CreateFlowRoleDto, FlowRoleDto, FlowRoleDto>> logger)
        : base(repository, logger)
    {
    }
}