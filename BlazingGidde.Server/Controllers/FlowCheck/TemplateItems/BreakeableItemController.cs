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
    public class BreakeableItemController : BlazingGiddeBaseController<BreakeableItem, ApplicationDbContext>
    {
        public BreakeableItemController(IRepository<BreakeableItem> repository, 
            ILogger<BlazingGiddeBaseController<BreakeableItem, ApplicationDbContext>> logger)
            : base(repository, logger)
        {
        }
    }
}
