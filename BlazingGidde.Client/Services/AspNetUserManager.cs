

using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services
{
    public class AspNetUserManager : APIRepository<FlowUser, string>
	{
		public AspNetUserManager(HttpClient _http)
			: base(_http, "AppUsers")
		{ }
	}


	
}
