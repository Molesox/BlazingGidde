using BlazingGidde.Server.Data;
using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.FlowCheck
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GazItemController : OnaBaseController<GazItem,int, ApplicationDbContext>
    {
        public GazItemController(IRepository<GazItem> repository, 
            ILogger<OnaBaseController<GazItem, int,ApplicationDbContext>> logger) 
            : base(repository, logger)
        {
        }
    }
}
