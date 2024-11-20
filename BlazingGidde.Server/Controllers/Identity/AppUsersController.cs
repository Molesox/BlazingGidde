using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingGidde.Server.Controllers.Identity
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AppUsersController : ControllerBase, IBlazingGiddeBaseController<IdentityUser>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AppUsersController> _logger;

        public AppUsersController(UserManager<IdentityUser> userManager, ILogger<AppUsersController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

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

        [HttpGet]
        public async Task<ActionResult<APIListOfEntityResponse<IdentityUser>>> GetAll()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                _logger.LogInformation("Successfully retrieved all users.");
                return OkListResponse(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all users.");
                return BadRequestListResponse<IdentityUser>(new[] { "An error occurred while processing your request." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<APIEntityResponse<IdentityUser>>> GetById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    _logger.LogWarning("User with Id {UserId} not found.", id);
                    return NotFoundResponse<IdentityUser>($"User with Id {id} not found.");
                }

                _logger.LogInformation("Successfully retrieved user with Id {UserId}.", id);
                return OkResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user with Id {UserId}.", id);
                return BadRequestResponse<IdentityUser>(new[] { "An error occurred while processing your request." });
            }
        }

        [HttpPost("getwithfilter")]
        public Task<ActionResult<APIListOfEntityResponse<IdentityUser>>> GetWithFilter(QueryFilter<IdentityUser> filter)
        {
            throw new NotImplementedException();
        }

        [HttpPost("getwithLinqfilter")]
        public async Task<ActionResult<APIListOfEntityResponse<IdentityUser>>> GetWithLinqFilter(LinqQueryFilter<IdentityUser> linqQueryFilter)
        {
            try
            {
                var users = await linqQueryFilter.GetFilteredList(_userManager.Users.AsQueryable());
                _logger.LogInformation("Successfully retrieved users with Linq filter.");
                return OkListResponse(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users with Linq filter.");
                return BadRequestListResponse<IdentityUser>(new[] { "An error occurred while processing your request." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<APIEntityResponse<IdentityUser>>> Post([FromBody] IdentityUser model)
        {
            try
            {

                // Create a new IdentityUser instance
                var user = new IdentityUser
                {
                    UserName = model.Email, // Or use a different username if needed
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                };

                // Create the user
                var result = await _userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to create user. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    return BadRequestResponse<IdentityUser>(result.Errors.Select(e => e.Description));
                }

                _logger.LogInformation("Successfully created user with Id {UserId}.", user.Id);
                return OkResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new user.");
                return BadRequestResponse<IdentityUser>(new[] { "An error occurred while processing your request." });
            }

        }

        [HttpPut]
        public async Task<ActionResult<APIEntityResponse<IdentityUser>>> Put([FromBody] IdentityUser user)
        {
            try
            {
                var isValid = ModelState.IsValid;
                var existingUser = await _userManager.FindByIdAsync(user.Id);

                if (existingUser == null)
                {
                    _logger.LogWarning("User with Id {UserId} not found for update.", user.Id);
                    return NotFoundResponse<IdentityUser>($"User with Id {user.Id} not found.");
                }

                existingUser.Email = user.Email;
                existingUser.UserName = user.UserName;
                existingUser.PhoneNumber = user.PhoneNumber;

                var result = await _userManager.UpdateAsync(existingUser);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to update user with Id {UserId}. Errors: {Errors}", user.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return BadRequestResponse<IdentityUser>(result.Errors.Select(e => e.Description));
                }

                _logger.LogInformation("Successfully updated user with Id {UserId}.", user.Id);
                return OkResponse(existingUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user with Id {UserId}.", user.Id);
                return BadRequestResponse<IdentityUser>(new[] { "An error occurred while processing your request." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIEntityResponse<IdentityUser>>> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    _logger.LogWarning("User with Id {UserId} not found for deletion.", id);
                    return NotFoundResponse<IdentityUser>($"User with Id {id} not found.");
                }

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to delete user with Id {UserId}. Errors: {Errors}", id, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return BadRequestResponse<IdentityUser>(result.Errors.Select(e => e.Description));
                }

                _logger.LogInformation("Successfully deleted user with Id {UserId}.", id);
                return OkResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user with Id {UserId}.", id);
                return BadRequestResponse<IdentityUser>(new[] { "An error occurred while processing your request." });
            }
        }
    }
}
