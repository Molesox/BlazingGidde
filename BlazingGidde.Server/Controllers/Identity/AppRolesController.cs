using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Server.Data;
using BlazingGidde.Shared.Models.Identity;
using BlazingGidde.Shared.Repository;
using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Server.Controllers.Identity
{

	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class AppRolesController : OnaBaseController<FlowRole, string, ApplicationDbContext>
	{
		public AppRolesController(IRepository<FlowRole> repository,
			ILogger<OnaBaseController<FlowRole, string, ApplicationDbContext>> logger
		)
			: base(repository, logger)
		{
		}


	}
}

