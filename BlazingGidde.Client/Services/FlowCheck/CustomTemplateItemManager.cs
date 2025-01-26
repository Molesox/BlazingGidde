using BlazingGidde.Shared.Models.FlowCheck;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck;

public class CustomTemplateItemManager : APIRepository<CustomTemplateItem, int>
{
    public CustomTemplateItemManager(HttpClient _http, IToastNotificationService toastNotificationService)
        : base(_http, "CustomTemplate", toastNotificationService)
    {
    }
}