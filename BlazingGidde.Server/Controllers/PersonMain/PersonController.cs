using System.Linq.Expressions;
using BlazingGidde.Client.Services;
using BlazingGidde.Server.Data;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Models.PersonMain;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingGidde.Server.Controllers.Identity
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : BlazingGiddeBaseController<Person, ApplicationDbContext>
    {
        private readonly RepositoryEF<Phone, ApplicationDbContext> _repositoryPhones;
        private readonly RepositoryEF<Email, ApplicationDbContext> _repositoryEmails;
        public PersonController(RepositoryEF<Person, ApplicationDbContext> repository,
        RepositoryEF<Phone, ApplicationDbContext> phonesRepository,
        RepositoryEF<Email, ApplicationDbContext> emailsRepository,
        ILogger<BlazingGiddeBaseController<Person, ApplicationDbContext>> logger)
        : base(repository, logger)
        {
            _repositoryPhones = phonesRepository;
            _repositoryEmails = emailsRepository;
        }

        [HttpGet("{id}/phones")]
        public async Task<ActionResult<APIListOfEntityResponse<Phone>>> GetPhonesByPersonId(int id)
        {
            try
            {
                var phones = await _repositoryPhones.dbSet.Where(phone => phone.PersonID == id).ToListAsync();
                return Ok(new APIListOfEntityResponse<Phone>
                {
                    Success = true,
                    Items = phones
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching phones for Person ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}/emails")]
        public async Task<ActionResult<APIListOfEntityResponse<Email>>> GetEmailsByPersonId(int id)
        {
            try
            {
                var emails = await _repositoryEmails.dbSet.Where(e => e.PersonID == id).ToListAsync();
                return Ok(new APIListOfEntityResponse<Email>
                {
                    Success = true,
                    Items = emails
                });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error fetching emails for Person ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
