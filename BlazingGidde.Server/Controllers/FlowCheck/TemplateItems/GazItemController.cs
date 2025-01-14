using BlazingGidde.Server.Data;
using BlazingGidde.Shared.DTOs.FlowCheck.TemplateItems;
using BlazingGidde.Shared.DTOs.FlowCheck.TemplateItems.Request;
using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.FlowCheck
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GazItemController : OnaBaseController<GazItem, int, ApplicationDbContext, GazItemDto, CreateGazItemDto>
    {
        public GazItemController(IRepository<GazItem> repository, ILogger<OnaBaseController<GazItem, int, ApplicationDbContext, GazItemDto, CreateGazItemDto, CreateGazItemDto, GazItemDto, GazItemDto>> logger) 
            : base(repository, logger)
        {
        }
    }
}
