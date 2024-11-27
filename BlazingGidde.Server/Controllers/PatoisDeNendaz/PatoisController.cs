using BlazingGidde.Server.Data;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Shared.Models.Patois;
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor;

namespace BlazingGidde.Server.Controllers.PatoisDeNendaz
{
	[Route("[Controller]")]
	[ApiController] 
	[Authorize]
	public class PatoisController : BlazingGiddeBaseController<DictionaryEntry, ApplicationDbContext>
	{
		public PatoisController(RepositoryEF<DictionaryEntry, ApplicationDbContext> repository,
			ILogger<BlazingGiddeBaseController<DictionaryEntry, ApplicationDbContext>> logger)
			: base(repository, logger)
		{
		}

		 
	}
}
