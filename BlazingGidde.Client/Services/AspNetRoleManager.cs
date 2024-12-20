using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Client.Services;

public class AspNetRoleManager : APIRepository<IdentityRole>
	{
		public AspNetRoleManager(HttpClient _http)
			: base(_http, "AppRoles", nameof(IdentityRole.Id))
		{ }
	}