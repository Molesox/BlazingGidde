using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class TemplateManager : APIRepository<Template, int>
    {
        public TemplateManager(HttpClient _http)
            : base(_http, "Template")
        { }
    }
}
