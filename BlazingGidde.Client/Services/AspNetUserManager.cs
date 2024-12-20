

using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services
{
    public class AspNetUserManager : APIRepository<FlowUser>
	{
		public AspNetUserManager(HttpClient _http)
			: base(_http, "AppUsers", nameof(FlowUser.Id))
		{ }
	}


	
}
