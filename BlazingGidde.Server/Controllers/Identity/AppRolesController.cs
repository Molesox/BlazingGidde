using System;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Repository;
using BlazingGidde.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingGidde.Shared.Models.Identity;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Server.Data;

namespace BlazingGidde.Server.Controllers.Identity
{

	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class AppRolesController : BlazingGiddeBaseController<IdentityRole, ApplicationDbContext>
	{
		public AppRolesController(RepositoryRole repository,
			ILogger<BlazingGiddeBaseController<IdentityRole, ApplicationDbContext>> logger
		)
			: base(repository, logger)
		{
		}


	}
}

