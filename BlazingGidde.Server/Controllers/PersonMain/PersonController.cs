using BlazingGidde.Client.Services;
using BlazingGidde.Server.Data;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.Identity
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : BlazingGiddeBaseController<Person, ApplicationDbContext>
    {
        public PersonController(RepositoryEF<Person, ApplicationDbContext> repository,
        ILogger<BlazingGiddeBaseController<Person, ApplicationDbContext>> logger)
        : base(repository, logger)
        {
        }
    }
}
