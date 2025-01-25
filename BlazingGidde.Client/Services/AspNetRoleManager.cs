using BlazingGidde.Shared.DTOs.FlowCheck;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services;

public class AspNetRoleManager : APIRepository<FlowRoleDto, string>
{
    public AspNetRoleManager(HttpClient _http, IToastNotificationService toastNotificationService)
        : base(_http, "AppRoles", toastNotificationService)
    {
    }
}