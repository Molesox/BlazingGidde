using System;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Repository;
using BlazingGidde.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingGidde.Shared.Models.Identity;

namespace BlazingGidde.Server.Controllers.Identity
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AppRolesController : ControllerBase, IBlazingGiddeBaseController<IdentityRole>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppRolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        [HttpGet("getroleusertree")]
        public async Task<IActionResult> GetRoleUserTree()
        {
            var roles = _roleManager.Roles.ToList();
            var treeData = new List<TreeNodeViewModel>();
            int idCounter = 1;

            foreach (var role in roles)
            {
                var roleId = idCounter++; // Generate a unique ID for each role
                treeData.Add(new TreeNodeViewModel
                {
                    Id = roleId,
                    Name = role.Name, // Role name
                    Email = null, // Roles don't have an email
                    ParentId = null // Top-level node
                });

                foreach (var user in _userManager.Users.ToList())
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        treeData.Add(new TreeNodeViewModel
                        {
                            Id = idCounter++, // Unique ID for each user
                            Name = user.UserName, // User name
                            Email = user.Email, // User email
                            ParentId = roleId // Child of the role
                        });
                    }
                }
            }

            return Ok(treeData); // Return as JSON
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

        private ActionResult<APIEntityResponse<T>> NotFoundResponse<T>(string errorMessage) where T : class
        {
            return NotFound(new APIEntityResponse<T>
            {
                Success = false,
                ErrorMessages = new List<string> { errorMessage }
            });
        }

        [HttpGet]
        public async Task<ActionResult<APIListOfEntityResponse<IdentityRole>>> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return OkListResponse(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<APIEntityResponse<IdentityRole>>> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFoundResponse<IdentityRole>($"Role with Id {id} not found.");
            }

            return OkResponse(role);
        }

        [HttpPost("getwithfilter")]
        public Task<ActionResult<APIListOfEntityResponse<IdentityRole>>> GetWithFilter(QueryFilter<IdentityRole> filter)
        {
            throw new NotImplementedException();
        }

        [HttpPost("getwithLinqfilter")]
        public async Task<ActionResult<APIListOfEntityResponse<IdentityRole>>> GetWithLinqFilter(LinqQueryFilter<IdentityRole> linqQueryFilter)
        {
            var roles = await linqQueryFilter.GetFilteredList(_roleManager.Roles.AsQueryable());
            return OkListResponse(roles);
        }

        [HttpPost]
        public async Task<ActionResult<APIEntityResponse<IdentityRole>>> Post([FromBody] IdentityRole role)
        {
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return BadRequestResponse<IdentityRole>(result.Errors.Select(e => e.Description));
            }

            return OkResponse(role);
        }

        [HttpPut]
        public async Task<ActionResult<APIEntityResponse<IdentityRole>>> Put([FromBody] IdentityRole role)
        {
            var existingRole = await _roleManager.FindByIdAsync(role.Id);

            if (existingRole == null)
            {
                return NotFoundResponse<IdentityRole>($"Role with Id {role.Id} not found.");
            }

            existingRole.Name = role.Name;
            existingRole.NormalizedName = role.Name.ToUpper();

            var updateResult = await _roleManager.UpdateAsync(existingRole);

            if (!updateResult.Succeeded)
            {
                return BadRequestResponse<IdentityRole>(updateResult.Errors.Select(e => e.Description));
            }

            return OkResponse(existingRole);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIEntityResponse<IdentityRole>>> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFoundResponse<IdentityRole>($"Role with Id {id} not found.");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                return BadRequestResponse<IdentityRole>(result.Errors.Select(e => e.Description));
            }

            return NoContent();
        }


    }
}

