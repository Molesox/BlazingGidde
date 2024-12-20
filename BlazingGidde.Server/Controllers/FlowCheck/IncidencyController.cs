using BlazingGidde.Server.Data;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.FlowCheck.TemplateItem
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class IncidencyController : BlazingGiddeBaseController<Incidency, ApplicationDbContext>
    {

        public IncidencyController(IRepository<Incidency> repository,
            ILogger<BlazingGiddeBaseController<Incidency, ApplicationDbContext>> logger)
            : base(repository, logger) { }
    }
}
