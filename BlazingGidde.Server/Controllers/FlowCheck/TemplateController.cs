using BlazingGidde.Server.Data;
using BlazingGidde.Shared.DTOs.FlowCheck;
using BlazingGidde.Shared.DTOs.FlowCheck.Request;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.FlowCheck
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TemplateController : OnaBaseController<Template, int, ApplicationDbContext, TemplateDto, CreateTemplateDto>
    {
        public TemplateController(IRepository<Template> repository, 
            ILogger<OnaBaseController<Template, int, ApplicationDbContext, TemplateDto, CreateTemplateDto, CreateTemplateDto, TemplateDto, TemplateDto>> logger) 
            : base(repository, logger)
        { }
    }
}
