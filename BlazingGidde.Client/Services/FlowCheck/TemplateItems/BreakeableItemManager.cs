using BlazingGidde.Shared.DTOs.FlowCheck.TemplateItems;
using BlazingGidde.Shared.DTOs.FlowCheck.TemplateItems.Request;
using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck.TemplateItems
{
    public class BreakeableItemManager : APIRepository<BreakeableItem, int, BreakableItemDto, CreateBreakableItemDto>
    {
        public BreakeableItemManager(HttpClient _http, IToastNotificationService toastNotificationService)
            : base(_http, "BreakeableItem", toastNotificationService)
        { }
    }
}
