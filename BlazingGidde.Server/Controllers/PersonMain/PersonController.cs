using BlazingGidde.Server.Data;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs.Person;
using BlazingGidde.Shared.DTOs.Person.Request;
using BlazingGidde.Shared.Models.PersonMain;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.Identity
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : OnaBaseController<Person, int, ApplicationDbContext, PersonDto, CreatePersonDto>
    {
        private readonly IRepository<Phone> _repositoryPhones;
        private readonly IRepository<Email> _repositoryEmails;
        public PersonController(IRepository<Person> repository,
        IRepository<Phone> phonesRepository,
        IRepository<Email> emailsRepository,
        ILogger<OnaBaseController<Person, int, ApplicationDbContext, PersonDto, CreatePersonDto>> logger)
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
                var linqFilter = new LinqQueryFilter<Phone>(p => p.PersonId == id);
                var phones =  _repositoryPhones.Get(linqFilter);
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
                var linqFilter = new LinqQueryFilter<Email>(p => p.PersonId == id);
                var emails =  _repositoryEmails.Get(linqFilter);
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
