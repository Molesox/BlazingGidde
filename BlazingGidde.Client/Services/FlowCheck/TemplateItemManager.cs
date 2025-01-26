using BlazingGidde.Shared.Models.FlowCheck;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck;

public class TemplateItemManager : APIRepository<TemplateItem, int>
{
    public TemplateItemManager(HttpClient _http, IToastNotificationService toastNotificationService)
        : base(_http, "TemplateItem", toastNotificationService)
    {
    }
}