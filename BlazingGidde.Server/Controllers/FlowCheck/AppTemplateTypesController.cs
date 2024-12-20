using System;
using BlazingGidde.Server.Data;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.FlowCheck;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AppTemplateTypesController : BlazingGiddeBaseController<TemplateType, ApplicationDbContext>
{
    public AppTemplateTypesController(IRepository<TemplateType> repository,
     ILogger<BlazingGiddeBaseController<TemplateType, ApplicationDbContext>> logger)
     : base(repository, logger)
    {
    }
}
