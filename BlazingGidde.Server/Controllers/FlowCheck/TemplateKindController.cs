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
    public class TemplateKindController : BlazingGiddeBaseController<TemplateKind, int, ApplicationDbContext>
    {

        public TemplateKindController(IRepository<TemplateKind> repository,
            ILogger<BlazingGiddeBaseController<TemplateKind,int, ApplicationDbContext>> logger)
            : base(repository, logger) { }
    }
}
