using BlazingGidde.Client.Services;
using BlazingGidde.Server.Data;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.PersonMain
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase, IBlazingGiddeBaseController<Person>
    {
        // Helper methods for consistent API responses
        private ActionResult<APIEntityResponse<T>> OkResponse<T>(T data) where T : class
        {
            return Ok(new APIEntityResponse<T>
            {
                Success = true,
                Items = data
            });
        }

        private ActionResult<APIListOfEntityResponse<T>> OkListResponse<T>(IEnumerable<T> data) where T : class
        {
            return Ok(new APIListOfEntityResponse<T>
            {
                Success = true,
                Items = data.ToList()
            });
        }

        private ActionResult<APIEntityResponse<T>> BadRequestResponse<T>(IEnumerable<string> errorMessages) where T : class
        {
            return BadRequest(new APIEntityResponse<T>
            {
                Success = false,
                ErrorMessages = errorMessages.ToList()
            });
        }
        private ActionResult<APIListOfEntityResponse<T>> BadRequestListResponse<T>(IEnumerable<string> errorMessages) where T : class
        {
            return BadRequest(new APIListOfEntityResponse<T>
            {
                Success = false,
                ErrorMessages = errorMessages.ToList()
            });
        }
        private ActionResult<APIEntityResponse<T>> NotFoundResponse<T>(string errorMessage) where T : class
        {
            return NotFound(new APIEntityResponse<T>
            {
                Success = false,
                ErrorMessages = new List<string> { errorMessage }
            });
        }



        ApplicationDbContext dbContext;
        public PersonController(ApplicationDbContext dBcontext)
        {
            this.dbContext = dBcontext;
        }

        public async Task<ActionResult<APIEntityResponse<Person>>> Delete(string Id)
        {

            throw new NotImplementedException();
        }
        [HttpGet]
        public async Task<ActionResult<APIListOfEntityResponse<Person>>> GetAll()
        {
         
                var persons = dbContext.Persons.ToList();
                return OkListResponse(persons);

             
           
        }

        public Task<ActionResult<APIEntityResponse<Person>>> GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<APIListOfEntityResponse<Person>>> GetWithFilter(Shared.Repository.QueryFilter<Person> Filter)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<APIListOfEntityResponse<Person>>> GetWithLinqFilter(Shared.Repository.LinqQueryFilter<Person> linqQueryFilter)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<APIEntityResponse<Person>>> Post([FromBody] Person Entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<APIEntityResponse<Person>>> Put([FromBody] Person Entity)
        {
            throw new NotImplementedException();
        }
    }
}
