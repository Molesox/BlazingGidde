 

using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Client.Services
{
	public class AspNetUserManager : APIRepository<IdentityUser>
	{
		public AspNetUserManager(HttpClient _http)
			: base(_http, "AppUsers", nameof(IdentityUser.Id))
		{ }
	}


	
}
