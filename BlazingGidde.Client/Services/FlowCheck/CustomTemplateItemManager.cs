using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class CustomTemplateItemManager : APIRepository<CustomTemplateItem>
    {
        public CustomTemplateItemManager(HttpClient _http)
            : base(_http, "AppCustomTemplates", nameof(CustomTemplateItem.Id))
        { }
    }
}
