using BlazingGidde.Server.Data;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Shared.Models.FlowCheck;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BlazingGidde.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AppCustomTemplatesController : BlazingGiddeBaseController<CustomTemplateItem, ApplicationDbContext>
    {

        public AppCustomTemplatesController(RepositoryEF<CustomTemplateItem, ApplicationDbContext> repository,
            ILogger<BlazingGiddeBaseController<CustomTemplateItem, ApplicationDbContext>> logger)
            : base(repository, logger) { }
    }
}
