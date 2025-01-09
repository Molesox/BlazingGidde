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
    public class BreakeableItemController : OnaBaseController<BreakeableItem, int,ApplicationDbContext>
    {
        public BreakeableItemController(IRepository<BreakeableItem> repository, 
            ILogger<OnaBaseController<BreakeableItem, int, ApplicationDbContext>> logger)
            : base(repository, logger)
        {
        }
    }
}
