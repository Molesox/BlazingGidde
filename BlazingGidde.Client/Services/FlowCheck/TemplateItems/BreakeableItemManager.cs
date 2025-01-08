using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck.TemplateItems
{
    public class BreakeableItemManager : APIRepository<BreakeableItem, int>
    {
        public BreakeableItemManager(HttpClient _http, IToastNotificationService toastNotificationService)
            : base(_http, "BreakeableItem", toastNotificationService)
        { }
    }
}
