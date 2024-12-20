using BlazingGidde.Server.Data;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Shared.Models.FlowCheck;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.Identity
{

    [ApiController]
	[Route("[controller]")]
	[Authorize]
	public class AppUsersController : BlazingGiddeBaseController<FlowUser, ApplicationDbContext>
	{
		public AppUsersController(RepositoryUser repository,
			ILogger<BlazingGiddeBaseController<FlowUser, ApplicationDbContext>> logger
		)
			: base(repository, logger)
		{
		}


	}
}