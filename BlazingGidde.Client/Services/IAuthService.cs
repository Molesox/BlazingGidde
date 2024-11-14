using BlazingGidde.Shared.Models.Identity;

namespace BlazingGidde.Client.Services
{
	public interface IAuthService
	{
		Task<LoginResult> Login(LoginModel loginModel);
		Task Logout();
		Task<RegisterResult> Register(RegisterModel registerModel);
	}
}
