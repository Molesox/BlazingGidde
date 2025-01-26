using BlazingGidde.Shared.DTOs.FlowCheck.TemplateItems;
using BlazingGidde.Shared.DTOs.FlowCheck.TemplateItems.Request;
using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck.TemplateItems;

public class GazItemManager : APIRepository<GazItem, int, GazItemDto, CreateGazItemDto>
{
    public GazItemManager(HttpClient _http, IToastNotificationService toastNotificationService)
        : base(_http, "GazItem", toastNotificationService)
    {
    }
}