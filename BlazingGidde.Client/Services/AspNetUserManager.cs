

using BlazingGidde.Shared.DTOs.FlowCheck;
using BlazingGidde.Shared.DTOs.FlowCheck.Request;
using BlazingGidde.Shared.Models.FlowCheck;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services
{
    public class AspNetUserManager : APIRepository<FlowUser, string, FlowUserDto, CreateFlowUserDto>
	{
		public AspNetUserManager(HttpClient _http, IToastNotificationService toastNotificationService)
			: base(_http, "AppUsers", toastNotificationService)
		{ }
	}


	
}
