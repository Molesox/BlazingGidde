

using BlazingGidde.Shared.Models.FlowCheck;
using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Client.Services
{
	public class AspNetUserManager : APIRepository<FlowUser>
	{
		public AspNetUserManager(HttpClient _http)
			: base(_http, "AppUsers", nameof(FlowUser.Id))
		{ }
	}


	
}
