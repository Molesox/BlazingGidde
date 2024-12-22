using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class CustomTemplateItemManager : APIRepository<CustomTemplateItem, int>
    {
        public CustomTemplateItemManager(HttpClient _http)
            : base(_http, "CustomTemplate")
        { }
    }
}
