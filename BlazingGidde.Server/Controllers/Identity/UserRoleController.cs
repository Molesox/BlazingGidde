using BlazingGidde.Server.Services;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.Identity;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserRoleController : ControllerBase
{
    private readonly ILogger<UserRoleController> _logger;
    private readonly UserRoleService _userRoleService;

    public UserRoleController(UserRoleService userRoleService, ILogger<UserRoleController> logger)
    {
        _userRoleService = userRoleService;
        _logger = logger;
    }

    [HttpPost("assign-role")]
    public async Task<ActionResult<APIResponse>> AssignRole([FromBody] AssignRoleDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.RoleName))
        {
            _logger.LogWarning("AssignRole called with invalid input.");
            return BadRequest(new APIResponse
            {
                Success = false,
                ErrorMessages = ["UserId and RoleName cannot be null or empty."]
            });
        }

        try
        {
            _logger.LogInformation("Assigning role '{RoleName}' to user with ID: {UserId}.", dto.RoleName, dto.UserId);
            var success = await _userRoleService.AssignUserToRole(dto.UserId, dto.RoleName);

            if (success)
            {
                _logger.LogInformation("Role '{RoleName}' assigned to user with ID: {UserId} successfully.",
                    dto.RoleName, dto.UserId);
                return Ok(new APIResponse
                {
                    Success = true
                });
            }

            _logger.LogWarning("Failed to assign role '{RoleName}' to user with ID: {UserId}.", dto.RoleName,
                dto.UserId);
            return BadRequest(new APIResponse
            {
                Success = false,
                ErrorMessages = ["Failed to assign role. Ensure the user and role exist."]
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while assigning role '{RoleName}' to user with ID: {UserId}.",
                dto.RoleName, dto.UserId);
            return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
            {
                Success = false,
                ErrorMessages = ["An internal server error occurred while assigning the role."]
            });
        }
    }

    [HttpPost("remove-role")]
    public async Task<ActionResult<APIResponse>> RemoveRole([FromBody] AssignRoleDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.RoleName))
        {
            _logger.LogWarning("RemoveRole called with invalid input.");
            return BadRequest(new APIResponse
            {
                Success = false,
                ErrorMessages = ["UserId and RoleName cannot be null or empty."]
            });
        }

        try
        {
            _logger.LogInformation("Removing role '{RoleName}' from user with ID: {UserId}.", dto.RoleName, dto.UserId);
            var success = await _userRoleService.RemoveUserFromRole(dto.UserId, dto.RoleName);

            if (success)
            {
                _logger.LogInformation("Role '{RoleName}' removed from user with ID: {UserId} successfully.",
                    dto.RoleName, dto.UserId);
                return Ok(new APIResponse
                {
                    Success = true
                });
            }

            _logger.LogWarning("Failed to remove role '{RoleName}' from user with ID: {UserId}.", dto.RoleName,
                dto.UserId);
            return BadRequest(new APIResponse
            {
                Success = false,
                ErrorMessages = ["Failed to remove role. Ensure the user and role exist."]
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while removing role '{RoleName}' from user with ID: {UserId}.",
                dto.RoleName, dto.UserId);
            return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
            {
                Success = false,
                ErrorMessages = ["An internal server error occurred while removing the role."]
            });
        }
    }

    [HttpGet("{userId}/roles")]
    public async Task<ActionResult<APIListOfEntityResponse<string>>> GetRoles(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogWarning("GetRoles called with null or empty UserId.");
            return BadRequest(new APIListOfEntityResponse<string>
            {
                Success = false,
                ErrorMessages = ["UserId cannot be null or empty."]
            });
        }

        try
        {
            _logger.LogInformation("Fetching roles for user with ID: {UserId}.", userId);
            var roles = await _userRoleService.GetRolesForUser(userId);

            if (roles != null && roles.Count > 0)
            {
                _logger.LogInformation("Roles for user with ID: {UserId} fetched successfully.", userId);
                return Ok(new APIListOfEntityResponse<string>
                {
                    Success = true,
                    Items = roles
                });
            }

            _logger.LogWarning("No roles found for user with ID: {UserId}.", userId);
            return Ok(new APIListOfEntityResponse<string>
            {
                Success = false,
                ErrorMessages = [$"No roles found for user with ID {userId}."]
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching roles for user with ID: {UserId}.", userId);
            return StatusCode(StatusCodes.Status500InternalServerError, new APIListOfEntityResponse<string>
            {
                Success = false,
                ErrorMessages = ["An internal server error occurred while fetching roles for the user."]
            });
        }
    }
}