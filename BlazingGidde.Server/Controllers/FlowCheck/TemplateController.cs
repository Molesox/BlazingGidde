using BlazingGidde.Server.Data;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.FlowCheck
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TemplateController : OnaBaseController<Template, int, ApplicationDbContext>
    {

        public TemplateController(IRepository<Template> repository,
            ILogger<OnaBaseController<Template, int, ApplicationDbContext>> logger)
            : base(repository, logger) { }

    }
}
