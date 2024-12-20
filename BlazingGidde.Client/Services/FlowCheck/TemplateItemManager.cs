using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class TemplateItemManager : APIRepository<TemplateItem>
    {
        public TemplateItemManager(HttpClient _http)
            : base(_http, "TemplateItem", nameof(TemplateItem.Id))
        { }
    }
}
