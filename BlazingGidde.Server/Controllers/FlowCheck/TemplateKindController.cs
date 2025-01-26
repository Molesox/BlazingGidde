using BlazingGidde.Server.Data;
using BlazingGidde.Shared.DTOs.FlowCheck;
using BlazingGidde.Shared.DTOs.FlowCheck.Request;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.FlowCheck;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TemplateKindController : OnaBaseController<TemplateKind, int, ApplicationDbContext, TemplateKindDto,
    CreateTemplateKindDto>
{
    public TemplateKindController(IRepository<TemplateKind> repository,
        ILogger<OnaBaseController<TemplateKind, int, ApplicationDbContext, TemplateKindDto, CreateTemplateKindDto,
            CreateTemplateKindDto, TemplateKindDto, TemplateKindDto>> logger)
        : base(repository, logger)
    {
    }
}