using BlazingGidde.Server.Data;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.Identity
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AppUsersController : BlazingGiddeBaseController<FlowUser, string, ApplicationDbContext>
    {
		public AppUsersController(IRepository<FlowUser> repository,
			ILogger<BlazingGiddeBaseController<FlowUser, string, ApplicationDbContext>> logger
		)
			: base(repository, logger)
		{
		}
    }
}