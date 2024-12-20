using BlazingGidde.Server.Data;
using BlazingGidde.Shared.Models.Patois;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.PatoisDeNendaz
{
    [Route("[Controller]")]
	[ApiController] 
	[Authorize]
	public class PatoisController : BlazingGiddeBaseController<DictionaryEntry, ApplicationDbContext>
	{
		public PatoisController(IRepository<DictionaryEntry> repository,
			ILogger<BlazingGiddeBaseController<DictionaryEntry, ApplicationDbContext>> logger)
			: base(repository, logger)
		{
		}

		 
	}
}
