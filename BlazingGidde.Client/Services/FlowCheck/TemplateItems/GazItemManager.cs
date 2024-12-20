using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;

namespace BlazingGidde.Client.Services.FlowCheck.TemplateItems
{
    public class GazItemManager : APIRepository<GazItem>
    {
        public GazItemManager(HttpClient _http)
            : base(_http, "GazItem", nameof(GazItem.Id))
        { }
    }
}
