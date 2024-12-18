using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class FlowUserManager : APIRepository<FlowUser>
    {
        public FlowUserManager(HttpClient _http)
            :base(_http, "AppFlowUsers", nameof(FlowUser.Id))
        { }
    }
}
