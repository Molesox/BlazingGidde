using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Client.Services;

public class AspNetRoleManager : APIRepository<FlowRole, string>
	{
		public AspNetRoleManager(HttpClient _http)
			: base(_http, "AppRoles")
		{

		}
	}