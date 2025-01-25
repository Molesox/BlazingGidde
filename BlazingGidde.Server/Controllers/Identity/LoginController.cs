using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlazingGidde.Server.Controllers.Identity;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly SignInManager<FlowUser> _signInManager;
    private readonly UserManager<FlowUser> _userManager;

    public LoginController(IConfiguration configuration,
        SignInManager<FlowUser> signInManager,
        UserManager<FlowUser> userManager)
    {
        _configuration = configuration;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
        if (!result.Succeeded)
            return BadRequest(new LoginResult
            {
                Successful = false,
                Error = "Username and password are invalid."
            });

        var user = await _userManager.FindByEmailAsync(login.Email);
        var claims = await _userManager.GetClaimsAsync(user);

        // Add user's email as Name claim
        claims.Add(new Claim(ClaimTypes.Name, user.Email));

        // Fetch roles and add each as a claim
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

        var token = new JwtSecurityToken(
            _configuration["JwtIssuer"],
            _configuration["JwtAudience"],
            claims,
            expires: expiry,
            signingCredentials: creds
        );

        return Ok(new LoginResult
        {
            Successful = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}