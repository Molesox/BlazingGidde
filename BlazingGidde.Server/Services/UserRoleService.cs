using BlazingGidde.Shared.Models.FlowCheck;
using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Server.Services;

public class UserRoleService
{
    private readonly RoleManager<FlowRole> _roleManager;
    private readonly UserManager<FlowUser> _userManager;

    public UserRoleService(UserManager<FlowUser> userManager, RoleManager<FlowRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> AssignUserToRole(string userId, string roleName)
    {
        // Find the user
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        // Ensure the role exists
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists) return false;

        // Add the user to the role
        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded;
    }

    public async Task<bool> RemoveUserFromRole(string userId, string roleName)
    {
        // Find the user
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        // Remove the user from the role
        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        return result.Succeeded;
    }

    public async Task<IList<string>> GetRolesForUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null ? await _userManager.GetRolesAsync(user) : new List<string>();
    }
}