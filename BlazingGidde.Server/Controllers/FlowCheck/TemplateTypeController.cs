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
public class TemplateTypeController : OnaTypeBaseController<TemplateType, TemplateTypeDto, CreateTemplateTypeDto>
{
    public TemplateTypeController(ITypeRepositoryEF<TemplateType > repository,
     ILogger<OnaBaseController<TemplateType,int, ApplicationDbContext, TemplateTypeDto, CreateTemplateTypeDto>> logger)
     : base(repository, logger)
    {
        
    }
}
