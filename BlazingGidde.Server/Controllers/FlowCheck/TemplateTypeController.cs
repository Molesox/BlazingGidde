using BlazingGidde.Server.Data;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.FlowCheck;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TemplateTypeController : BlazingGiddeBaseController<TemplateType, int, ApplicationDbContext>
{
    public TemplateTypeController(IRepository<TemplateType> repository,
     ILogger<BlazingGiddeBaseController<TemplateType,int, ApplicationDbContext>> logger)
     : base(repository, logger)
    {
    }
}
