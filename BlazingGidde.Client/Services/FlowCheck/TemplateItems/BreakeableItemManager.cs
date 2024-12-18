using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;

namespace BlazingGidde.Client.Services.FlowCheck.TemplateItems
{
    public class BreakeableItemManager : APIRepository<BreakeableItem>
    {
        public BreakeableItemManager(HttpClient _http)
            : base(_http, "AppBreakeableItems", nameof(BreakeableItem.Id))
        { }
    }
}
