using BlazingGidde.Server.Data;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BlazingGidde.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CustomTemplateController : OnaBaseController<CustomTemplateItem , int, ApplicationDbContext>
    {

        public CustomTemplateController(IRepository<CustomTemplateItem> repository,
            ILogger<OnaBaseController<CustomTemplateItem,int, ApplicationDbContext>> logger)
            : base(repository, logger) { }
    }

}

